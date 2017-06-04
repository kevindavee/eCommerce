using eCommerce.Core.CommerceClasses.Banks;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Banks
{
    public class BankRepo : RepoBase<Bank>
    {
        public BankRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
