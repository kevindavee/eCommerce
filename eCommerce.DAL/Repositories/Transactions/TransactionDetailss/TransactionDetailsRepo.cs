using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using Microsoft.EntityFrameworkCore;
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

        public List<TransactionDetails> GetByHeaderId(long TransactionHeaderId)
        {
            return dbSet.Where(s => s.TransactionHeaderId == TransactionHeaderId)
                        .Include(i => i.ProductInstance)
                        .Include(i => i.ProductInstance.Product)
                        .Include(i => i.ProductInstance.ProductInstanceOptions).ToList();
        }
    }
}
