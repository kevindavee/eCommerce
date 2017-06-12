using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Brands;
using Microsoft.AspNetCore.Authorization;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.Web.Models.HomeViewModels;
using eCommerce.DAL.Repositories.The_Products.Categories;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private BrandRepo brandRepo;
        private ProductRepo productRepo;
        private CategoryRepo categoryRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;

        string userName = "";

        public HomeController(BrandRepo _brandRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo, ProductInstanceOptionsRepo _productInstanceOptionsRepo, CategoryRepo _categoryRepo)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
            this.productInstanceOptionsRepo = _productInstanceOptionsRepo;
            this.categoryRepo = _categoryRepo;
        }
        public ActionResult Index()
        {
            var categoryList = categoryRepo.GetAll();

            var model = new HomeViewModel();
            model.CategoryList = categoryList.ToList();

            return View(model);
        }


        public PartialViewResult CategoryList()
        {
            var categoryList = categoryRepo.GetAll();

            return PartialView(categoryList.ToList());
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

        
    }
}
