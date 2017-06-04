using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;
using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;

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

            if (model.CurrentStatus == TransactionStatus.CheckedOut)
            {
                RedirectToAction("ShippingInformation", new { transaction = model});
            }

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
                catch (Exception ex)
                {
                    return Json(data: new { Status = false, ErrorMsg = ex.Message });
                }
            }

            return Json(data: new { Status = true, ErrorMsg = ""});
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
            var transaction = transactionHeaderRepo.GetById(TransactionHeaderId);

            if (transaction != null)
            {
                transaction.LastStatus = transaction.CurrentStatus;
                transaction.CurrentStatus = TransactionStatus.CheckedOut;

                try
                {
                    transactionHeaderRepo.Save(transaction);
                    return RedirectToAction("ShippingInformation", new { transaction = transaction });

                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                }
            }

            return RedirectToAction("Cart", new { CustomerId = CustomerId });
        }

        public ActionResult ShippingInformation(TransactionHeader transaction)
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