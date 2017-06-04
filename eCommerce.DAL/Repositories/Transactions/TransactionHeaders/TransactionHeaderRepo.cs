using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.TransactionHeaders
{
    public class TransactionHeaderRepo : RepoBase<TransactionHeader>
    {
        public TransactionHeaderRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
