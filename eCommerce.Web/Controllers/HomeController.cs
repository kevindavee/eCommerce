using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Brands;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private BrandRepo brandRepo;
        string userName = "";

        public HomeController(BrandRepo _brandRepo)
        {
            brandRepo = _brandRepo;
        }
        public IActionResult Index()
        {
            var brandList = brandRepo.GetAll();
            return View();
        }
    }
}
