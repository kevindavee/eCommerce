using eCommerce.Core.CommerceClasses.The_Products.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.DAL.Repositories.The_Products.Products
{
    public class ProductInstanceOptionsRepo
    {
        private CommerceContext context;
        //private DbSet<ProductInstanceOptions> dbSet;
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
                //var InstanceOptions = dbSet.Where(j => j.ProductInstanceId == itemId).ToList();
                var InstanceOptionsUkuran = dbSet.Where(j => j.ProductInstanceId == itemId && j.OptionValue.Value == optValueWarna).ToList();
                var InstanceOptionsWarna = dbSet.Where(j => j.ProductInstanceId == itemId && j.OptionValue.Value == optValueUkuran).ToList();
                if (InstanceOptionsUkuran.Count() > 0 && InstanceOptionsWarna.Count() > 0)
                {
                    ChoosenIdForProductInstance = itemId;
                    break;
                }
            }

            return ChoosenIdForProductInstance;
        }
        public async Task<List<ProductInstanceOptions>> GetOptionValueByInstanceIdAsync(List<long> ProductInstanceIds)
        {
            List<ProductInstanceOptions> list = new List<ProductInstanceOptions>();

            var instancesList = await dbSet.Include(i => i.OptionValue).Include(i => i.OptionValue.Options).ToListAsync();

            foreach (var item in ProductInstanceIds)
            {
                list.AddRange(instancesList.Where(s => s.ProductInstanceId == item).ToList());
            }

            return list;
        }
    }
}
