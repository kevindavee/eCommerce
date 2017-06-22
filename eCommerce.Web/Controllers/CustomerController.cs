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

namespace eCommerce.Web.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerRepo customerRepo;
        private AlamatRepo alamatRepo;


        public CustomerController(CustomerRepo _customerRepo, AlamatRepo _alamatRepo)
        {
            this.customerRepo = _customerRepo;
            this.alamatRepo = _alamatRepo;
        }

        #region Profile
        public ActionResult Profile(long CustomerId = 0)
        {
            var customer = customerRepo.GetById(CustomerId);

            var model = new ProfileViewModel();
            model.Customer = customer;
            
            //Page edit profile customer
            return View(model);
        }

        [HttpPost]
        public ActionResult Profile(ProfileViewModel model)
        {
            var customer = customerRepo.GetById(model.Customer.Id);
            try
            {
                customer.Nama = model.Customer.Nama;
                customer.JenisKelamin = model.Customer.JenisKelamin;
                customer.TanggalLahir = model.Customer.TanggalLahir;
                customer.NoTelepon = model.Customer.NoTelepon;
                customer.StatusNikah = model.Customer.StatusNikah;
                customer.Pekerjaan = model.Customer.Pekerjaan;

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
        public ActionResult AddressList(long CustomerId)
        {
            var listAlamat = alamatRepo.GetAlamatForCurrentCustomer(CustomerId);
            
            //Page yang berisi list address user
            var model = new DaftarAlamatViewModel();
            model.ListAlamat = listAlamat;
            return View(model);
        }

        public ActionResult DetailsAlamat(long AlamatId)
        {

            var alamatCustomer = alamatRepo.GetById(AlamatId);

            var model = new AlamatViewModel();
            model.Alamat = alamatCustomer;
            //model.CustomerId =

            return View("DetailsAlamat");
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
                    AlamatCustomer.CustomerId = model.CustomerId;
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

            return RedirectToAction("AddressList", new { CustomerId = AlamatCustomer.CustomerId});
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