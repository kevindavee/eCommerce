using eCommerce.DAL.Repositories.Transactions.ShippingDetailss;
using eCommerce.Web.Models.AdminTransaction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.AdminTransaction
{
    public class ShippingListViewComponent: ViewComponent
    {
        private ShippingDetailsRepo shippingDetailsRepo;

        public ShippingListViewComponent(ShippingDetailsRepo _shippingDetailsRepo)
        {
            shippingDetailsRepo = _shippingDetailsRepo;
        }

        public IViewComponentResult Invoke()
        {
            ShippingDetailsViewModel viewmodel = new ShippingDetailsViewModel();
            viewmodel.ShippingDetails = shippingDetailsRepo.GetProcessedShippingDetails();

            return View("ShippingList", viewmodel);
        }
    }
}
