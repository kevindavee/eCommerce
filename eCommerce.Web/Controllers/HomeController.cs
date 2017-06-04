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

        string userName = "";

        public HomeController(BrandRepo _brandRepo, ProductRepo _productRepo)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
        }
        public IActionResult Index(long brandId = 0, decimal minPrice = 0, decimal maxPrice = 0)
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
