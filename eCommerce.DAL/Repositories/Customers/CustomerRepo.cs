using eCommerce.Core.CommerceClasses.Customers;
using eCommerce.Core.ICommerceRepositories.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Customers
{
    public class CustomerRepo : RepoBase<Customer>, ICustomerRepo
    {
        public CustomerRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
