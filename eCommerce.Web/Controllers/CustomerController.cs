using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Customers;
using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Customers;
using eCommerce.Web.Models.CustomerViewModels;
using eCommerce.DAL.Repositories.Alamats;
using eCommerce.Core.CommerceClasses.Alamats;
using eCommerce.DAL.Repositories.UserLogins;
using Microsoft.AspNetCore.Http;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;

namespace eCommerce.Web.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepo customerRepo;
        private TransactionHeaderRepo transactionHeaderRepo;
        private AlamatRepo alamatRepo;
        private UserManagementRepo userRepo;
        private IHttpContextAccessor context;

        string UserName = "";
        long CustomerId = 0;

        public CustomerController(CustomerRepo _customerRepo, AlamatRepo _alamatRepo, UserManagementRepo _userRepo, IHttpContextAccessor _context,
            TransactionHeaderRepo _transactionHeaderRepo)
        {
            this.customerRepo = _customerRepo;
            this.alamatRepo = _alamatRepo;
            this.transactionHeaderRepo = _transactionHeaderRepo;
            context = _context;
            UserName = context.HttpContext.User.Identity.Name;
            CustomerId = _userRepo.GetCustomerId(UserName);
        }

        #region Profile
        public ActionResult Profile()
        {
            var customer = customerRepo.GetById(CustomerId);

            var model = new ProfileViewModel();
            model.Customer = customer;
            model.Day = customer.TanggalLahir.Day;
            model.Month = customer.TanggalLahir.Month;
            model.Year = customer.TanggalLahir.Year;


            //Page edit profile customer
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveProfile(ProfileViewModel model)
        {
            var customer = customerRepo.GetById(model.Customer.Id);
            try
            {
                var bday = DateTime.Parse(model.Month + "/" + model.Day + "/" + model.Year, System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);


                customer.Nama = model.Customer.Nama;
                customer.JenisKelamin = model.Customer.JenisKelamin;
                customer.TanggalLahir = bday;
                customer.NoTelepon = model.Customer.NoTelepon;
                customer.StatusNikah = model.Customer.StatusNikah;
                customer.Pekerjaan = (model.Customer.Pekerjaan == "Lainnya" ? model.JobLainnya : model.Customer.Pekerjaan);

                customerRepo.Save(customer);
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Profile", new { CustomerId = model.Customer.Id });
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
        public ActionResult AddressList()
        {
            var listAlamat = alamatRepo.GetAlamatForCurrentCustomer(CustomerId);

            //Page yang berisi list address user
            var model = new DaftarAlamatViewModel();
            model.ListAlamat = listAlamat;
            model.CustomerId = CustomerId;

            return View(model);
        }

        public ActionResult DetailsAlamat(long AlamatId)
        {

            var alamatCustomer = alamatRepo.GetById(AlamatId);

            var model = new AlamatViewModel();
            model.Alamat = alamatCustomer;
            model.CustomerId = CustomerId;

            return View("DetailsAlamat", model);
        }

        [HttpPost]
        public ActionResult DeleteCustomerAddress(long AddressId)
        {
            var AlamatCustomer = alamatRepo.GetById(AddressId);

            try
            {
                AlamatCustomer.Deleted = true;

                alamatRepo.Save(AlamatCustomer);
            }
            catch (Exception ex)
            {

                throw;
            }
            return RedirectToAction("AddressList", new {  });
        }

        [HttpPost]
        public ActionResult SaveCustomerAddress(AlamatViewModel model)
        {
            var AlamatCustomer = alamatRepo.GetById(model.Alamat.Id);
            try
            {
                if (AlamatCustomer != null)
                {
                    AlamatCustomer.NamaAlamat = model.Alamat.NamaAlamat;
                    AlamatCustomer.TheAlamat = model.Alamat.TheAlamat;
                    AlamatCustomer.KodePos = model.Alamat.KodePos;
                    AlamatCustomer.Kota = model.Alamat.Kota;
                    AlamatCustomer.Provinsi = model.Alamat.Provinsi;

                }
                else
                {
                    AlamatCustomer = new Alamat();
                    AlamatCustomer.CustomerId = CustomerId;
                    AlamatCustomer.NamaAlamat = model.Alamat.NamaAlamat;
                    AlamatCustomer.TheAlamat = model.Alamat.TheAlamat;
                    AlamatCustomer.KodePos = model.Alamat.KodePos;
                    AlamatCustomer.Kota = model.Alamat.Kota;
                    AlamatCustomer.Provinsi = model.Alamat.Provinsi;

                }
                alamatRepo.Save(AlamatCustomer);


            }
            catch (Exception ex)
            {

                throw;
            }

            return RedirectToAction("AddressList", new { });
        }


        [HttpPost]
        public ActionResult AddNewAddress(AlamatViewModel model)
        {
            try
            {
                var newAddress = new Alamat();
                newAddress.CustomerId = model.CustomerId;
                newAddress.NamaAlamat = model.Alamat.NamaAlamat;
                newAddress.TheAlamat = model.Alamat.TheAlamat;
                newAddress.KodePos = model.Alamat.KodePos;
                newAddress.Kota = model.Alamat.Kota;
                newAddress.Provinsi = model.Alamat.Provinsi;


                alamatRepo.Save(newAddress);

                return RedirectToAction("AddressList", new { CustomerId = newAddress.CustomerId });
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Transaction History
        public ActionResult Transaction()
        {
            //var TransactionList = transactionHeaderRepo.GetTransactionsHistory(CustomerId);

            var model = new TransactionHistoryViewModel();
            //model.ListTransaction = TransactionList;

            //Page untuk list transaksi yang sudah selesai, transaksi yang sedang berjalan, dan transaksi yang di reject
            return View(model);
        }

        public ActionResult TransactionDetails(long TransactionId)
        {
            var Transaction = transactionHeaderRepo.GetDetailsTransactionsHistory(TransactionId);

            var model = new DetailsTransactionHistoryViewModel();
            model.TransactionHeader = Transaction;

            //Page untuk detail transaksi
            return View(model);
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