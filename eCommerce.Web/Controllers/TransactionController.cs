using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class TransactionController : Controller
    {
        #region Shopping Cart
        public ActionResult Cart()
        {
            //Page shopping cart
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFromCart()
        {
            //Action untuk delete item dari cart
            return View();
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