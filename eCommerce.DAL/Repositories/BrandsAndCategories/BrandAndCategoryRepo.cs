using eCommerce.Core.CommerceClasses.BrandsAndCategories;
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
    }
}
