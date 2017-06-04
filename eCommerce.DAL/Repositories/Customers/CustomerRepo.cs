using eCommerce.Core.CommerceClasses.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Customers
{
    public class CustomerRepo : RepoBase<Customer>
    {
        public CustomerRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
