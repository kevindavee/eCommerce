using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.TransactionHeaders
{
    public class TransactionHeaderRepo : RepoBase<TransactionHeader>
    {
        public TransactionHeaderRepo(CommerceContext _context) : base(_context)
        {
        }

        /// <summary>
        /// Get customer's active cart
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public TransactionHeader GetActiveCart(long CustomerId)
        {
            var cart = dbSet.Where(s => s.CurrentStatus == TransactionStatus.OnCart &&
                                         s.LastStatus == TransactionStatus.OnCart &&
                                         s.CustomerId == CustomerId)
                             .Include(i => i.TransactionDetails)
                             .FirstOrDefault();

            var checkedOut = dbSet.Where(s => s.CurrentStatus == TransactionStatus.CheckedOut &&
                                              s.LastStatus == TransactionStatus.OnCart &&
                                              s.CustomerId == CustomerId)
                                  .Include(i => i.TransactionDetails)
                                  .FirstOrDefault();

            if (checkedOut != null)
            {
                cart = checkedOut;
            }

            return cart;
        }
    }
}
