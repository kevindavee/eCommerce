using eCommerce.DAL.Repositories.Banks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.AdminTransaction
{
    public class BankListViewComponent: ViewComponent
    {
        private BankRepo bankRepo;
        public BankListViewComponent(BankRepo _bankRepo)
        {
            bankRepo = _bankRepo;
        }

        public IViewComponentResult Invoke()
        {
            var model = bankRepo.GetAll();
            
            return View("BankList", model);
        }
    }
}
