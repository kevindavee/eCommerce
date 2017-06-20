using eCommerce.Core.CommerceClasses.The_Products.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Products
{
    public class ProductRepo : RepoBase<Product>
    {
        public ProductRepo(CommerceContext _context) : base(_context)
        {
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return dbSet.Include(i => i.Brand).Include(i => i.Category);
        }

        public Product GetByIdIncludeCat(long id)
        {
            return dbSet.Where(i => i.Id == id).Include(i => i.Category).FirstOrDefault();
        }
    }
}
