using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Products
{
    public class ProductInstanceRepo : RepoBase<ProductInstance>
    {
        public ProductInstanceRepo(CommerceContext _context) : base(_context)
        {
            
        }
        public decimal GetPriceForProductList(long productId)
        {
            decimal price;
            var productInstance = dbSet.Where(j => j.ProductId == productId).OrderBy(j => j.Price).FirstOrDefault();
            if (productInstance != null)
            {
                price = productInstance.Price;
            }
            else
            {
                price = 0;
            }

            return price;
        }
    }
}
