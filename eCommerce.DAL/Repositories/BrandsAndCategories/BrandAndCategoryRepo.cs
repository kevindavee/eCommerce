using eCommerce.Core.CommerceClasses.BrandsAndCategories;
using System;
using System.Collections.Generic;
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
    }
}
