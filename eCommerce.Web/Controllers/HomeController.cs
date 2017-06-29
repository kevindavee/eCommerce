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
            var model = new HomeViewModel();

            model.Product1 = productRepo.GetByCategoryIncludeImage(3).OrderBy(o => o.Seen).Take(4).ToList();
            model.Product2 = productRepo.GetByCategoryIncludeImage(7).OrderBy(o => o.Seen).Take(4).ToList();
            model.Product3 = productRepo.GetByCategoryIncludeImage(10).OrderBy(o => o.Seen).Take(4).ToList();
            return View(model);
        }

        
    }
}
