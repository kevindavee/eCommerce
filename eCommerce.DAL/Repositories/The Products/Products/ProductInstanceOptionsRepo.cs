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
        protected DbSet<OptionValue> dbSetOptValue;


        public ProductInstanceOptionsRepo(CommerceContext _context)
        {
            context = _context;
            dbSet = context.Set<ProductInstanceOptions>();
            dbSetOptValue = context.Set<OptionValue>();
        }

        public void Save(ProductInstanceOptions prodInstanceOpt)
        {
            context.ProductInstanceOptions.Add(prodInstanceOpt);
            context.SaveChanges();
        }
        public IEnumerable<ProductInstanceOptions> GetByProductInstanceIdAndOptionId(long productInstanceId, long OptionId)
        {
            return dbSet.Where(i => i.ProductInstanceId == productInstanceId && i.OptionValue.OptionsId == OptionId);
        }
        public IEnumerable<ProductInstanceOptions> GetByProductInstanceId(long productInstanceId)
        {
            return dbSet.Where(i => i.ProductInstanceId == productInstanceId);
        }

        public void DeleteListProductInstanceOptions(List<ProductInstanceOptions> listInstanceOptions)
        {
            dbSet.RemoveRange(listInstanceOptions);
            context.SaveChanges();
        }

        public long GetPriceByFilter(long productId, string optValueWarna, string optValueUkuran, string parentCategory)
        {
            var listProductInstanceId = dbSet.Where(j => j.ProductInstance.ProductId == productId)
                                                    .Select(j => j.ProductInstanceId)
                                                    .Distinct()
                                                    .ToList();

            long ChoosenIdForProductInstance = 0;
            foreach (var itemId in listProductInstanceId)
            {
                //var InstanceOptions = dbSet.Where(j => j.ProductInstanceId == itemId).ToList();
                var InstanceOptionsUkuran = dbSet.Where(j => j.ProductInstanceId == itemId && j.OptionValue.Value == optValueUkuran).ToList();
                var InstanceOptionsWarna = dbSet.Where(j => j.ProductInstanceId == itemId && j.OptionValue.Value == optValueWarna).ToList();

                //untuk product yang non-elektronik
                if (parentCategory != "Elektronik")
                {
                    if (InstanceOptionsUkuran.Count() > 0 && InstanceOptionsWarna.Count() > 0)
                    {
                        ChoosenIdForProductInstance = itemId;
                        break;
                    }
                }
                //untuk product yang elektronik
                else
                {
                    if (InstanceOptionsWarna.Count() > 0)
                    {
                        ChoosenIdForProductInstance = itemId;
                        break;
                    }
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

        public string GetWarnaByProductInstanceId(long ProductInstanceId)
        {
            string warna = "";
            var InstanceOptionsWarna = dbSet.Where(j => j.ProductInstanceId == ProductInstanceId && j.OptionValue.Options.OptionName == "Warna").FirstOrDefault();
            warna = dbSetOptValue.Find(InstanceOptionsWarna.OptionValueId).Value;

            return warna;
        }


        public string GetUkuranByProductInstanceId(long ProductInstanceId)
        {
            string ukuran = "";
            var InstanceOptionsUkuran = dbSet.Where(j => j.ProductInstanceId == ProductInstanceId && j.OptionValue.Options.OptionName == "Ukuran").FirstOrDefault();
            ukuran = dbSetOptValue.Find(InstanceOptionsUkuran.OptionValueId).Value;

            return ukuran;
        }
    }
}
