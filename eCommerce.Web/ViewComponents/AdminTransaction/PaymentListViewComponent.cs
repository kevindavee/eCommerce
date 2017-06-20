using eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans;
using eCommerce.DAL.Repositories.Transactions.ShippingDetailss;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.Web.Models.AdminTransaction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.AdminTransaction
{
    public class PaymentListViewComponent: ViewComponent
    {
        private KonfirmasiPembayaranRepo konfirmasiPembayaranRepo;
        private ShippingDetailsRepo shippingDetailsRepo;
        private TransactionHeaderRepo transactionHeaderRepo;

        public PaymentListViewComponent(KonfirmasiPembayaranRepo _konfirmasiPembayaranRepo, ShippingDetailsRepo _shippingDetailsRepo, 
                                        TransactionHeaderRepo _transactionHeaderRepo)
        {
            konfirmasiPembayaranRepo = _konfirmasiPembayaranRepo;
            shippingDetailsRepo = _shippingDetailsRepo;
            transactionHeaderRepo = _transactionHeaderRepo;
        }

        public IViewComponentResult Invoke()
        {
            ManagePaymentViewModel viewModel = new ManagePaymentViewModel();

            viewModel.konfirmasiPembayaran = konfirmasiPembayaranRepo.GetActiveList();

            return View("PaymentList", viewModel);
        }
    }
}
