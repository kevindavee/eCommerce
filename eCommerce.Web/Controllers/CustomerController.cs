using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Customers;
using eCommerce.Core.ICommerceRepositories.Customers;
using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Customers;

namespace eCommerce.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepo customerRepo;

        public CustomerController(ICustomerRepo _customerRepo)
        {
            this.customerRepo = _customerRepo;
        }

        #region Profile
        public ActionResult Profile(long CustomerId)
        {
            //Page edit profile customer
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile()
        {
            return RedirectToAction("Profile", new { });
        }

        #endregion

        #region Change Password
        public ActionResult ChangePassword(long CustomerId)
        {
            //Page edit profile customer
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword()
        {
            return RedirectToAction("Profile", new { });
        }
        #endregion

        #region Address
        public ActionResult AddressList(long CustomerId)
        {
            //Page yang berisi list address user
            return View();
        }

        public ActionResult UpdateAddress(long CustomerId)
        {
            //Form add-edit address user
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAddress()
        {
            return RedirectToAction("AddressList", new { });
        }
        #endregion

        #region Transaction History
        public ActionResult Transaction(long CustomerId)
        {
            //Page untuk list transaksi yang sudah selesai, transaksi yang sedang berjalan, dan transaksi yang di reject
            return View();
        }

        public ActionResult TransactionDetail(long TransactionId)
        {
            //Page untuk detail transaksi
            return View();
        }

        #endregion

        #region Wishlist
        public ActionResult WishList(long CustomerId)
        {
            //Page untuk melihat wishlist
            return View();
        }
        #endregion
    }
}