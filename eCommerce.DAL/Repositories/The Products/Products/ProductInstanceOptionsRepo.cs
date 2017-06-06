using eCommerce.Core.CommerceClasses.The_Products.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Products
{
    public class ProductInstanceOptionsRepo
    {
        private CommerceContext context;
        private DbSet<ProductInstanceOptions> dbSet;
        public ProductInstanceOptionsRepo(CommerceContext _context)
        {
            context = _context;
            dbSet = context.Set<ProductInstanceOptions>();
            
        }

        public List<ProductInstanceOptions> GetOptionValueByInstanceId(List<long> ProductInstanceIds)
        {
            List<ProductInstanceOptions> list = new List<ProductInstanceOptions>();

            foreach (var item in ProductInstanceIds)
            {
                list.AddRange(dbSet.Where(s => s.ProductInstanceId == item)
                         .Include(i => i.OptionValue)
                         .Include(i => i.OptionValue.Options)
                         .ToList());
            }
            
            return list;
        }
    }
}
