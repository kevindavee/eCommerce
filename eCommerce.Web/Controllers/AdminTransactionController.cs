using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web.Controllers
{
    public class AdminTransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}