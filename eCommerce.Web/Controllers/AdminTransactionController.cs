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

        public PartialViewResult PaymentList(DateTime? StartDate, DateTime? EndDate)
        {
            ManagePaymentViewModel viewModel = new ManagePaymentViewModel();

            viewModel.konfirmasiPembayaran = konfirmasiPembayaranRepo.GetActiveList();
            viewModel.transactionHeader = transactionHeaderRepo.GetTransactionsWaitingForApproval();

            return PartialView("_PaymentList", viewModel);
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

        public PartialViewResult ShipmentList()
        {
            return PartialView();
        }

        public ActionResult ManageBank()
        {
            return View();
        }

        public ActionResult AddBank()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBank(Bank bank)
        {
            return RedirectToAction("ManageBank");
        }

        public ActionResult ManageShipper()
        {
            return View();
        }

        public ActionResult AddShipper()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShipper(Shipper shipper)
        {
            return RedirectToAction("ManageShipper");
        }
    }
}