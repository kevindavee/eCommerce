using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Brands;
using eCommerce.DAL.Repositories.The_Products.Products;

namespace eCommerce.Web.Controllers
{
    public class AdminProductController : Controller
    {
        private BrandRepo brandRepo;
        private ProductRepo productRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;

        public AdminProductController(BrandRepo _brandRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo, ProductInstanceOptionsRepo _productInstanceOptionsRepo)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
            this.productInstanceOptionsRepo = _productInstanceOptionsRepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageProduct()
        {
            //Page untuk melihat list of product
            return View();
        }

        public PartialViewResult ProductList()
        {
            //Partial page untuk refresh hasil filter product
            return PartialView();
        }

        public ActionResult UpdateProduct()
        {
            //Form untuk input product baru atau edit product
            return View();
        }

        
    }
}