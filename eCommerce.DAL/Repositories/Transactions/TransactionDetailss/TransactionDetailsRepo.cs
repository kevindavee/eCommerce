using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.TransactionDetailss
{
    public class TransactionDetailsRepo : RepoBase<TransactionDetails>
    {
        public TransactionDetailsRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
