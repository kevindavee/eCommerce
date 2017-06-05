using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.TransactionDetailss
{
    public class TransactionDetailsRepo : RepoBase<TransactionDetails>
    {
        public TransactionDetailsRepo(CommerceContext _context) : base(_context)
        {
        }

        public decimal CalculateTotalPrice(long TransactionHeaderId)
        {
            decimal TotalPrice = 0;
            var Items = dbSet.Where(s => s.TransactionHeaderId == TransactionHeaderId).ToList();

            foreach (var item in Items)
            {
                TotalPrice += item.Price;
            }

            return TotalPrice;
        }
    }
}
