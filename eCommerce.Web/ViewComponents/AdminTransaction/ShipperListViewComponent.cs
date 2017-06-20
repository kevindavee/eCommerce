using eCommerce.DAL.Repositories.Shippers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.AdminTransaction
{
    public class ShipperListViewComponent: ViewComponent
    {
        private ShipperRepo shipperRepo;
        public ShipperListViewComponent(ShipperRepo _shipperRepo)
        {
            shipperRepo = _shipperRepo;
        }

        public IViewComponentResult Invoke()
        {
            var model = shipperRepo.GetAll();

            return View("ShipperList", model);
        }
    }
}
