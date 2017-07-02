using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.UserLogins;
using eCommerce.Web.Models.CustomerViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.Customer
{
    public class CurrentTransactionViewComponent : ViewComponent
    {
        private TransactionHeaderRepo transactionHeaderRepo;
        private IHttpContextAccessor context;

        string UserName = "";
        long CustomerId = 0;

        public CurrentTransactionViewComponent(TransactionHeaderRepo _transactionHeaderRepo, UserManagementRepo _userRepo, IHttpContextAccessor _context)
        {
            transactionHeaderRepo = _transactionHeaderRepo;
            context = _context;
            UserName = context.HttpContext.User.Identity.Name;
            CustomerId = _userRepo.GetCustomerId(UserName);
        }

        public IViewComponentResult Invoke()
        {
            var TransactionList = transactionHeaderRepo.GetCurrentTransactionsHistory(CustomerId);

            var model = new TransactionHistoryViewModel();
            model.ListTransaction = TransactionList;

            return View("CurrentTransaction", model);
        }
    }
}
