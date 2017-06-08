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
        protected DbSet<ProductInstanceOptions> dbSet;

        public ProductInstanceOptionsRepo(CommerceContext _context)
        {
            context = _context;
            dbSet = context.Set<ProductInstanceOptions>();
        }

        public long GetPriceByFilter(long productId, string optValueWarna, string optValueUkuran)
        {
            var listProductInstanceId = dbSet.Where(j => j.ProductInstance.ProductId == productId)
                                                    .Select(j => j.ProductInstanceId)
                                                    .Distinct()
                                                    .ToList();

            long ChoosenIdForProductInstance = 0;
            foreach (var itemId in listProductInstanceId)
            {
                var InstanceOptions = dbSet.Where(j => j.ProductInstanceId == itemId);
                var InstanceOptionsUkuran = InstanceOptions.Where(j => j.OptionValue.Value == optValueWarna);
                var InstanceOptionsWarna = InstanceOptions.Where(j => j.OptionValue.Value == optValueUkuran);
                if (InstanceOptionsUkuran != null && InstanceOptionsWarna != null)
                {
                    ChoosenIdForProductInstance = itemId;
                    break;
                }
            }

            return ChoosenIdForProductInstance;
        }
    }
}
