using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL;

namespace eCommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            //Page product index
            return View();
        }

        public ActionResult ProductIndex()
        {
            //Partial view untuk refresh list of product
            return PartialView();
        }

        public ActionResult Detail(long ProductId)
        {
            return View();
        }
    }
}