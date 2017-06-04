using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.KumpulanRepos.Brands;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private BrandRepo brandRepo;

        public HomeController(BrandRepo _brandRepo)
        {
            brandRepo = _brandRepo;
        }
        public IActionResult Index()
        {
            //Home Page

            var brandList = brandRepo.GetAll();

            return View();
        }
    }
}
