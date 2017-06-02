using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class AdminProductController : Controller
    {
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