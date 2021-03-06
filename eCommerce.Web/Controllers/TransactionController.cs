using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;
using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.Shippers;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCommerce.Web.Models.TransactionViewModels;
using eCommerce.DAL.Repositories.Alamats;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans;
using eCommerce.DAL.Repositories.Banks;
using Microsoft.AspNetCore.Http;
using eCommerce.DAL.Repositories.UserLogins;
using eCommerce.DAL.Repositories.Customers;
using eCommerce.Logic.Services;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.Core.CommerceClasses.Banks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using eCommerce.Core.CommerceClasses.Shippers;
using eCommerce.Core.CommerceClasses.Alamats;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Web.Controllers
{
    public class TransactionController : Controller
    {
        private AlamatRepo alamatRepo;
        private BankRepo bankRepo;
        private KonfirmasiPembayaranRepo konfirmasiPembayaranRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;
        private TransactionHeaderRepo transactionHeaderRepo;
        private TransactionDetailsRepo transactionDetailRepo;
        private ShipperRepo shipperRepo;
        private UserManagementRepo userRepo;

        private TransactionService transactionService;
        private IHostingEnvironment environment;
        private IHttpContextAccessor context;

        string UserName = "";
        long CustomerId = 0;

        public TransactionController(TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailRepo, ShipperRepo _shipperRepo,
                                     AlamatRepo _alamatRepo, KonfirmasiPembayaranRepo _konfirmasiPembayaranRepo, BankRepo _bankRepo, UserManagementRepo _userRepo,
                                     TransactionService _transactionService, ProductInstanceOptionsRepo _productInstanceOptionsRepo, IHostingEnvironment _environment,
                                     IHttpContextAccessor _context)
        {
            transactionHeaderRepo = _transactionHeaderRepo;
            transactionDetailRepo = _transactionDetailRepo;
            shipperRepo = _shipperRepo;
            alamatRepo = _alamatRepo;
            konfirmasiPembayaranRepo = _konfirmasiPembayaranRepo;
            bankRepo = _bankRepo;
            userRepo = _userRepo;
            transactionService = _transactionService;
            productInstanceOptionsRepo = _productInstanceOptionsRepo;
            environment = _environment;
            context = _context;
            UserName = context.HttpContext.User.Identity.Name;
            CustomerId = _userRepo.GetCustomerId(UserName);
        }

        #region Shopping Cart
        [Authorize]
        [HttpPost]
        public JsonResult AddToCart(long ProductInstanceId, int Quantity)
        {
            //Action untuk add Item ke shopping cart
            string result;
            //CustomerId = 1;
            var CustomerId = userRepo.GetCustomerId(User.Identity.Name);
            var activeCart = transactionHeaderRepo.GetActiveCart(CustomerId);

            if (activeCart == null)
            {
                result = transactionService.CreateTransaction(CustomerId, ProductInstanceId, Quantity, UserName);
            }
            else
            {
                result = transactionService.AddItemToCart(activeCart, ProductInstanceId, Quantity, UserName);
            }

            if (result == FunctionResult.OutOfStock)
            {
                return Json(data: new { Status = false, EmptyStock = "Cannot add to cart ! Please refresh your page" });
            }
            if (result == FunctionResult.Error)
            {
                return Json(data: new { Status = false, ErrorMsg = "Error !" });
            }

            return Json(data: new { Status = true });
        }

        [Authorize]
        public ActionResult Cart()
        {
            //Page shopping cart
            ShoppingCartViewModel viewmodel = new ShoppingCartViewModel();

            //CustomerId = 1;
            var model = transactionHeaderRepo.GetActiveCart(CustomerId);
            if (model != null)
            {
                model.TransactionDetails = transactionDetailRepo.GetByHeaderId(model.Id);

                var InstanceIds = model.TransactionDetails.Select(s => s.ProductInstanceId).ToList();

                viewmodel.TransactionHeader = model;
                viewmodel.ProductInstanceOptions = productInstanceOptionsRepo.GetOptionValueByInstanceIdAsync(InstanceIds).Result;


                if (viewmodel.TransactionHeader.CurrentStatus == TransactionStatus.CheckedOut)
                {
                    return RedirectToAction("CheckOutForm", new { TransactionHeaderId = viewmodel.TransactionHeader.Id });
                }
            }
            else
            {
                viewmodel = null;
            }

            return View(viewmodel);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UpdateQuantity(int Quantity, long TransactionDetailId)
        {
            //Action untuk update quantity item di cart
            //Pake AJAX supaya tidak refresh page
            var result = transactionService.UpdateQuantity(Quantity, TransactionDetailId, UserName);

            if (result == FunctionResult.Error)
            {
                return Json(data: new { Status = false, ErrorMsg = "An error ocurred !" });
            }
            if (result == FunctionResult.OutOfStock)
            {
                return Json(data: new { Status = false, EmptyStock = "Insufficient quantity for this item" });
            }

            var headerId = transactionDetailRepo.GetById(TransactionDetailId).TransactionHeaderId;
            var TotalPrice = transactionHeaderRepo.GetById(headerId).TotalPrice;

            return Json(data: new { Status = true, TotalPrice = TotalPrice });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromCart(long TransactionDetailId, long TransactionHeaderId)
        {
            //Action untuk delete item dari cart
            //Pake AJAX supaya tidak refresh page
            //var CustomerId = transactionHeaderRepo.GetById(TransactionHeaderId).CustomerId;

            var result = transactionService.DeleteItem(TransactionDetailId, TransactionHeaderId, UserName);

            if (!result)
            {
                ViewData["Message"] = "Cannot delete item from cart !";
            }

            return RedirectToAction("Cart", new { CustomerId = CustomerId });
        }
        #endregion

        #region CheckOut
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(long TransactionHeaderId)
        {
            //CustomerId = 1;
            //Action untuk checkout item dari cart
            try
            {
                //TODO : Ganti customer dengan Username
                transactionHeaderRepo.ChangeStatus(TransactionHeaderId, TransactionStatus.CheckedOut, UserName);

                //Uncoment saat sudah bisa pakai username
                //CustomerId = transaction.CustomerId;

                return RedirectToAction("CheckOutForm", new { TransactionHeaderId = TransactionHeaderId });

            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }


            return RedirectToAction("Cart", new { CustomerId = CustomerId });
        }

        [Authorize]
        public ActionResult CheckOutForm(long TransactionHeaderId)
        {
            //Page setelah klik button checkout. Untuk melakukan pembayaran dan melengkapi data pengiriman
            var shipper = new List<Shipper>();
            shipper = shipperRepo.GetAll();
            shipper.Insert(0, new Shipper { Id = 0, Nama = "Choose shipper" });

            var alamat = new List<Alamat>();
            alamat = alamatRepo.GetAlamatForCurrentCustomer(CustomerId);
            alamat.Insert(0, new Alamat { Id = 0, NamaAlamat = "Choose Address" });

            ViewBag.Shipper = shipper;
            ViewBag.Alamat = alamat;

            CheckOutViewModel viewmodel = new CheckOutViewModel();

            viewmodel.Transaction = transactionHeaderRepo.GetTransactionFullDataById(TransactionHeaderId);
            

            if (viewmodel.Transaction == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (viewmodel.Transaction.CurrentStatus != TransactionStatus.CheckedOut)
            {
                if (viewmodel.Transaction.CurrentStatus == TransactionStatus.OnCart)
                {
                    return RedirectToAction("Cart", new { CustomerId = viewmodel.Transaction.CustomerId });
                }
                else if (viewmodel.Transaction.CurrentStatus == TransactionStatus.PaymentConfirmation)
                {
                    return RedirectToAction("PaymentConfirmation", new { TransactionHeaderId = viewmodel.Transaction.Id });
                }
                else
                {
                    return RedirectToAction("Home", "Index");
                }
            }
            return View(viewmodel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmTransaction(CheckOutViewModel checkOutViewModel, long TransactionHeaderId)
        {
            //Action untuk memfinalisasi transaksi
            var shipping = checkOutViewModel.ShippingDetail;
            shipping.TransactionHeaderId = TransactionHeaderId;

            //Hapus jika dropdown sudah berfungsi
            shipping.CreatedBy = shipping.UpdatedBy = UserName;
            shipping.CreatedDate = shipping.UpdatedDate = DateTime.Today;

            var result = transactionService.ConfirmTransaction(TransactionHeaderId, shipping, UserName);

            if (!result)
            {
                ViewData["Message"] = "Cannot process order !";

                var shipper = new List<Shipper>();
                shipper = shipperRepo.GetAll();
                shipper.Insert(0, new Shipper { Id = 0, Nama = "Choose shipper" });

                var alamat = new List<Alamat>();
                alamat = alamatRepo.GetAll().Where(s => s.CustomerId == CustomerId).ToList();
                alamat.Insert(0, new Alamat { Id = 0, NamaAlamat = "Choose Address" });

                ViewBag.Shipper = shipper;
                ViewBag.Alamat = alamat;

                return RedirectToAction("CheckOutForm", new { TransactionHeaderId = TransactionHeaderId });
            }

            return RedirectToAction("TransactionSubmitted");
        }

        [Authorize]
        public ActionResult TransactionSubmitted(long TransactionId)
        {
            //Page untuk menampilkan bahwa transaksi sudah berhasil. Menampilkan nomor rekening untuk customer bisa transfer
            var model = bankRepo.GetAll();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelCheckOut(long TransactionHeaderId)
        {
            try
            {
                transactionHeaderRepo.ChangeStatus(TransactionHeaderId, TransactionStatus.OnCart, UserName);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return RedirectToAction("CheckOutForm", new { TransactionHeaderId = TransactionHeaderId });
            }

            return RedirectToAction("Cart", new { CustomerId = CustomerId });
        }

        public JsonResult GetAlamatDetail(long AlamatId)
        {
            var result = alamatRepo.GetById(AlamatId);

            return Json(data: new { TheAlamat = result.TheAlamat, Kota = result.Kota, Provinsi = result.Provinsi, KodePos = result.KodePos });
        }

        #endregion

        #region Payment Confirmation
        [Authorize]
        public ActionResult PaymentConfirmation(long TransactionHeaderId)
        {
            //Page untuk customer mengisi form konfirmasi pembayaran
            KonfirmasiPembayaran model = new KonfirmasiPembayaran();

            var bank = new List<Bank>();
            bank = bankRepo.GetAll();
            bank.Insert(0, new Bank { Id = 0, Nama = "Choose bank" });
            ViewBag.Bank = bank;

            if (TransactionHeaderId > 0)
            {
                var isWaiting = transactionHeaderRepo.IsWaitingForConfirmation(TransactionHeaderId, CustomerId);
                if (isWaiting == false)
                {
                    ViewData["Message"] = "Your already confirmed your order !";
                    return RedirectToAction("Index", "Home");
                }

                model.TransactionHeaderId = TransactionHeaderId;
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentConfirmation(KonfirmasiPembayaran konfirmasiPembayaran, IFormFile upload)
        {
            //Action untuk submit konfirmasi pembayaran
            try
            {
                if (upload != null)
                {
                    var filePathAndName = Path.Combine(environment.WebRootPath, "BuktiTransfer", upload.FileName);
                    using (var stream = new FileStream(filePathAndName, FileMode.Create))
                    {
                        upload.CopyTo(stream);
                    }
                    konfirmasiPembayaran.ImageBuktiTransfer = "/BuktiTransfer/" + upload.FileName;
                }

                konfirmasiPembayaran.CreatedBy = konfirmasiPembayaran.UpdatedBy = UserName;
                konfirmasiPembayaranRepo.Save(konfirmasiPembayaran);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Cannot save this data !\nError: " + ex.Message;

                var bank = new List<Bank>();
                bank = bankRepo.GetAll();
                bank.Insert(0, new Bank { Id = 0, Nama = "Choose bank" });
                ViewBag.Bank = bank;

                return View(konfirmasiPembayaran);
            }

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}