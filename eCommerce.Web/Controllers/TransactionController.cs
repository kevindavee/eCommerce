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

        public TransactionController(TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailRepo, ShipperRepo _shipperRepo,
                                     AlamatRepo _alamatRepo, KonfirmasiPembayaranRepo _konfirmasiPembayaranRepo, BankRepo _bankRepo, UserManagementRepo _userRepo,
                                     TransactionService _transactionService, ProductInstanceOptionsRepo _productInstanceOptionsRepo)
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
        }

        #region Shopping Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddToCart(long ProductInstanceId, int Quantity)
        {
            //Action untuk add Item ke shopping cart
            bool result;
            var CustomerId = userRepo.GetCustomerId(User.Identity.Name);
            var activeCart = transactionHeaderRepo.GetActiveCart(CustomerId);

            if (activeCart == null)
            {
                result = transactionService.CreateTransaction(CustomerId, ProductInstanceId, Quantity);
            }
            else
            {
                result = transactionService.AddItemToCart(activeCart, ProductInstanceId, Quantity);
            }

            return Json(data: new { Status = result });
        }

        public ActionResult Cart(long CustomerId)
        {
            //Page shopping cart
            ShoppingCartViewModel viewmodel = new ShoppingCartViewModel();
            CustomerId = 1;
            var model = transactionHeaderRepo.GetActiveCart(CustomerId);
            model.TransactionDetails = transactionDetailRepo.GetByHeaderId(model.Id);

            var InstanceIds = model.TransactionDetails.Select(s => s.ProductInstanceId).ToList();

            viewmodel.TransactionHeader = model;
            viewmodel.ProductInstanceOptions = productInstanceOptionsRepo.GetOptionValueByInstanceId(InstanceIds);
            

            if (viewmodel.TransactionHeader.CurrentStatus == TransactionStatus.CheckedOut)
            {
                RedirectToAction("ShippingInformation", new { transaction = viewmodel.TransactionHeader});
            }

            return View(viewmodel);
        }

        [HttpPost]
        public JsonResult UpdateQuantity(int Quantity, long TransactionDetailId)
        {
            //Action untuk update quantity item di cart
            //Pake AJAX supaya tidak refresh page
            var result = transactionService.UpdateQuantity(Quantity, TransactionDetailId);

            if (!result)
            {
                return Json(data: new { Status = false, ErrorMsg = "An error ocurred !" });
            }

            var detail = transactionDetailRepo.GetById(TransactionDetailId);
            var itemPrice = detail.Price;
            var TotalPrice = transactionHeaderRepo.GetById(detail.TransactionHeaderId).TotalPrice;

            return Json(data: new { Status = true, ErrorMsg = "", itemPrice = itemPrice, TotalPrice = TotalPrice});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteFromCart(long TransactionDetailId)
        {
            //Action untuk delete item dari cart
            //Pake AJAX supaya tidak refresh page
            
            try
            {
                transactionDetailRepo.Delete(TransactionDetailId);
            }
            catch (Exception ex)
            {
                return Json(data: new { Status = false, ErrorMsg = ex.Message });
            }

            return Json(data: new { Status = true, ErrorMsg = "" });
        }
        #endregion

        #region CheckOut
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(long TransactionHeaderId, long CustomerId)
        {
            //Action untuk checkout item dari cart
            try
            {
                var transaction = transactionHeaderRepo.ChangeStatus(TransactionHeaderId, TransactionStatus.CheckedOut);

                transactionHeaderRepo.Save(transaction);
                return RedirectToAction("ShippingInformation", new { TransactionHeaderId = transaction.Id });

            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
            

            return RedirectToAction("Cart", new { CustomerId = CustomerId });
        }

        public ActionResult ShippingInformation(long TransactionHeaderId)
        {
            //Page setelah klik button checkout. Untuk melakukan pembayaran dan melengkapi data pengiriman
            ShippingInfoViewModel viewmodel = new ShippingInfoViewModel();

            ViewBag.Shipper = new SelectList(shipperRepo.GetAll(), "Id", "Nama");
            ViewBag.Alamat = new SelectList(alamatRepo.GetAll(), "Id", "Nama");

            viewmodel.Transaction = transactionHeaderRepo.GetById(TransactionHeaderId);

            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmTransaction()
        {
            //Action untuk memfinalisasi transaksi

            return RedirectToAction("TransactionSubmitted");
        }

        public ActionResult TransactionSubmitted(long TransactionId)
        {
            //Page untuk menampilkan bahwa transaksi sudah berhasil. Menampilkan nomor rekening untuk customer bisa transfer
            return View();
        }

        #endregion

        #region Payment Confirmation
        public ActionResult PaymentConfirmation(long TransactionHeaderId)
        {
            //Page untuk customer mengisi form konfirmasi pembayaran
            KonfirmasiPembayaran model = new KonfirmasiPembayaran();
            ViewBag.Bank = new SelectList(bankRepo.GetAll(), "Id", "Nama");

            if (TransactionHeaderId > 0)
            {
                var isWaiting = transactionHeaderRepo.IsWaitingForConfirmation(TransactionHeaderId);
                if (isWaiting == false)
                {
                    TempData["Message"] = "Invalid transaction. Cannot confirm payment";
                    return RedirectToAction("Index", "Home");
                }

                model.TransactionHeaderId = TransactionHeaderId;
            }

            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentConfirmation(KonfirmasiPembayaran konfirmasipembayaran, IFormFile upload)
        {
            //Action untuk submit konfirmasi pembayaran
            try
            {
                konfirmasiPembayaranRepo.Save(konfirmasipembayaran);
                //Upload image

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error" + ex.Message;
                return View(konfirmasipembayaran);
            }

            return RedirectToAction("Transaction", "Customer", new { CustomerId = userRepo.GetCustomerId(User.Identity.Name)});
        }
        #endregion
    }
}