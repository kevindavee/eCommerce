using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.KumpulanRepos.Brands;
using eCommerce.Core.ICommerceRepositories.Brands;

namespace eCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private IBrandRepo brandRepo;

        public HomeController(IBrandRepo _brandRepo)
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
