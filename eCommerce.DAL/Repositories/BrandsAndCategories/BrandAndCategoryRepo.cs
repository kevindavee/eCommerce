using eCommerce.Core.CommerceClasses.BrandsAndCategories;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.BrandsAndCategories
{
    public class BrandAndCategoryRepo
    {
        private CommerceContext context;
        public BrandAndCategoryRepo(CommerceContext _context)
        {
            context = _context;
        }

        public IEnumerable<BrandAndCategory> GetByCategoryId(long catId)
        {
            return context.BrandAndCategory.Where(i => i.CategoryId == catId);
        }

        public List<Category> GetCategoriesByBrandId(long BrandId)
        {
            return context.BrandAndCategory.Where(s => s.BrandId == BrandId).Select(s => s.Category).ToList();
        }

        /// <summary>
        /// Return true if the data is valid and saved. Return false if there is duplicate data
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Save(BrandAndCategory entity)
        {
            if (context.BrandAndCategory.Where(s => s.CategoryId == entity.CategoryId && s.BrandId == entity.BrandId).SingleOrDefault() == null)
            {
                context.BrandAndCategory.Add(entity);
                context.SaveChanges();

                return true;
            }

            return false;
        }

        public void Delete(BrandAndCategory entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
