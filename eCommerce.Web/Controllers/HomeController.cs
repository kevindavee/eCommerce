using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Brands;
using Microsoft.AspNetCore.Authorization;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.Web.Models.HomeViewModels;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private BrandRepo brandRepo;
        private ProductRepo productRepo;
        private ProductInstanceRepo productInstanceRepo;
        string userName = "";

        public HomeController(BrandRepo _brandRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult HomeView(long brandId = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            var brandList = brandRepo.GetAll().ToList();

            //get product list 
            //bisa difilter lewat brand juga
            //untuk filter price-nya masih bingung price taro dimana.
            var productList = productRepo.GetAll().Where(j => brandId == 0 ? true : j.BrandId == brandId).ToList();

            //untuk product harus pake paging. 
            //bisa paging dengan sakura.pagedlist
            //bisa juga paging dengan ReflectionIT.Mvc.Paging. 
            var model = new HomeViewModel();
            model.BrandList = brandList;
            model.ProductList = productList;

            return View();
        }

        //Untuk Pilihan Per-Category Brand
        public ActionResult HomePerCategoriesView(long CategoryId = 0)
        {
            var brandList = brandRepo.GetAll().ToList();
            var productList = productRepo.GetAll().Where(j => CategoryId == 0 ? true : j.CategoryId == CategoryId).ToList();


            var model = new HomeViewModel();
            model.BrandList = brandList;
            model.ProductList = productList;
            return View();
        }


        //Jika Product di Click akan menuju details product
        public ActionResult DetailsProductView(long ProductId = 0)
        {
            var productObj = productRepo.GetById(ProductId);


            var model = new ProductDetailsViewModel();
            model.Product = productObj;
            return View();
        }


        //Get Product Per-Options
        //Jadi method nya nanti akan diisi jika user milih sebuah product 
        //dengan ukuran dan warna tertentu bisa saja terjadi perubahan harga
        public JsonResult GetPriceByOptions(long ProductId = 0, string optValueWarna = "", string optValueUkuran = "")
        {
            //var ProductInstanceObj = productInstanceRepo.GetAll()
            //                                            .Where(j => j.ProductId == ProductId);


            return Json("");
        }
    }
}
