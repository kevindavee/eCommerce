using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Products
{
    public class OptionValueRepo : RepoBase<OptionValue>
    {
        public OptionValueRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
