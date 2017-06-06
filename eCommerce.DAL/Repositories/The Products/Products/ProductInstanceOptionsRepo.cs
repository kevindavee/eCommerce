using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Products
{
    public class ProductInstanceOptionsRepo
    {
        private CommerceContext context;
        public ProductInstanceOptionsRepo(CommerceContext _context)
        {
            context = _context;
        }
    }
}
