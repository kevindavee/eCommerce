using eCommerce.DAL.Repositories.Brands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.AdminProduct
{
    public class ManageBrandListViewComponent: ViewComponent
    {
        private BrandRepo brandRepo;

        public ManageBrandListViewComponent(BrandRepo _brandRepo)
        {
            brandRepo = _brandRepo;
        }

        public IViewComponentResult Invoke()
        {
            var model = brandRepo.GetAll();

            return View("ManageBrandList", model);
        }
    }
}
