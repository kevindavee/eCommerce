using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;

namespace eCommerce.Web.Controllers
{
    public class TransactionController : Controller
    {
        private TransactionHeaderRepo transactionHeaderRepo;
        private TransactionDetailsRepo transactionDetailRepo;

        public TransactionController(TransactionHeaderRepo _transactionHeaderRepo, TransactionDetailsRepo _transactionDetailRepo)
        {
            this.transactionHeaderRepo = _transactionHeaderRepo;
            this.transactionDetailRepo = _transactionDetailRepo;
        }
        #region Shopping Cart
        public ActionResult Cart(long CustomerId)
        {
            //Page shopping cart
            var model = transactionHeaderRepo.GetActiveCart(CustomerId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateQuantity(int Quantity, long TransactionDetailId)
        {
            //Action untuk update quantity item di cart
            //Pake AJAX supaya tidak refresh page
            var item = transactionDetailRepo.GetById(TransactionDetailId);
            if (item != null)
            {
                item.Quantity = Quantity;

                try
                {
                    transactionDetailRepo.Save(item);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Json();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteFromCart()
        {
            //Action untuk delete item dari cart
            //Pake AJAX supaya tidak refresh page
            return Json();
        }
        #endregion

        #region CheckOut
        [HttpPost]
        public ActionResult CheckOut()
        {
            //Action untuk checkout item dari cart
            return RedirectToAction("ShippingInformation");
        }

        public ActionResult ShippingInformation()
        {
            //Page setelah klik button checkout. Untuk melakukan pembayaran dan melengkapi data pengiriman
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmTransaction()
        {
            //Action untuk memfinalisasi transaksi
            return RedirectToAction("TransactionSubmitted");
        }

        public ActionResult TransactionSubmitted()
        {
            //Page untuk menampilkan bahwa transaksi sudah berhasil. Menampilkan nomor rekening untuk customer bisa transfer
            return View();
        }

        #endregion

        #region Payment Confirmation
        public ActionResult PaymentConfirmation(long CustomerI)
        {
            //Page untuk customer mengisi form konfirmasi pembayaran
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentConfirmation()
        {
            //ACtion untuk submit konfirmasi pembayaran
            return View();
        }
        #endregion
    }
}