using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Core.CommerceClasses.Banks;
using eCommerce.Core.CommerceClasses.Shippers;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;
using eCommerce.DAL.Repositories.Banks;
using eCommerce.DAL.Repositories.Shippers;
using eCommerce.DAL.Repositories.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans;
using eCommerce.Web.Models.AdminTransaction;
using eCommerce.Commons;
using Microsoft.AspNetCore.Http;
using eCommerce.Logic.Services;

namespace eCommerce.Web.Controllers
{
    public class AdminTransactionController : Controller
    {
        private BankRepo bankRepo;
        private KonfirmasiPembayaranRepo konfirmasiPembayaranRepo;
        private TransactionHeaderRepo transactionHeaderRepo;
        private TransactionDetailsRepo transactionDetailsRepo;
        private ShipperRepo shipperRepo;
        private ShippingDetailsRepo shippingDetailsRepo;

        private TransactionService transactionService;
        private IHttpContextAccessor context;

        string Username = "";


        public AdminTransactionController(BankRepo _bankRepo, TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailsRepo,
                                            ShipperRepo _shipperRepo, ShippingDetailsRepo _shippingDetailsRepo, KonfirmasiPembayaranRepo _konfirmasiPembayaran,
                                            IHttpContextAccessor _context, TransactionService _transactionService)
        {
            bankRepo = _bankRepo;
            transactionHeaderRepo = _transactionHeaderRepo;
            transactionDetailsRepo = _transactionDetailsRepo;
            shipperRepo = _shipperRepo;
            shippingDetailsRepo = _shippingDetailsRepo;
            konfirmasiPembayaranRepo = _konfirmasiPembayaran;
            transactionService = _transactionService;
            context = _context;
            Username = context.HttpContext.User.Identity.Name;
        }

        public ActionResult ManagePayment()
        {
            return View();
        }

        public IActionResult PaymentList(DateTime? StartDate, DateTime? EndDate)
        {
            return ViewComponent("PaymentList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidatePaymentConfirmation(long TransactionHeaderId, string Remarks, long KonfirmasiPembayaranId, bool Validation)
        {
            var result = transactionService.ProcessConfirmation(TransactionHeaderId, Remarks, Username, KonfirmasiPembayaranId, Validation);

            if (!result)
            {
                ViewData["Message"] = "An error occured. Cannot updated data !";
                return RedirectToAction("ManagePayment");
            }

            return RedirectToAction("ManagePayment");
        }

        public ActionResult ManageShipment()
        {
            return View();
        }

        public IActionResult ShipmentList()
        {
            return ViewComponent("ShippingList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateShippingStatus(long ShippingDetailsId, string TrackingNumber)
        {
            shippingDetailsRepo.UpdateTrackingNumber(ShippingDetailsId, TrackingNumber, Username);

            return RedirectToAction("ManageShipment");
        }

        public ActionResult ManageBank()
        {
            var model = bankRepo.GetAll();

            return View(model);
        }

        public ActionResult AddBank(long BankId = 0)
        {
            Bank bank;
            if (BankId == 0)
            {
                bank = new Bank();
            }
            else
            {
                bank = bankRepo.GetById(BankId);
            }

            return View(bank);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBank(Bank bank)
        {
            if (bank.Id != 0)
            {
                bank.UpdatedBy = "Admin";
                bank.UpdatedDate = DateTime.Today;

            }
            bankRepo.Save(bank);

            return RedirectToAction("ManageBank");
        }

        public ActionResult ManageShipper()
        {
            var model = shipperRepo.GetAll();

            return View(model);
        }

        public ActionResult AddShipper(long ShipperId = 0)
        {
            Shipper shipper;
            if (ShipperId == 0)
            {
                shipper = new Shipper();
            }
            else
            {
                shipper = shipperRepo.GetById(ShipperId);
            }

            return View(shipper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShipper(Shipper shipper)
        {
            if (shipper.Id != 0)
            {
                shipper.UpdatedBy = "Admin";
                shipper.UpdatedDate = DateTime.Today;

            }
            shipperRepo.Save(shipper);
            return RedirectToAction("ManageShipper");
        }
    }
}