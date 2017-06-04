using eCommerce.Core.CommerceClasses.BrandsAndCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.BrandsAndCategories
{
    public class BrandAndCategoryRepo : RepoBase<BrandAndCategory>
    {
        public BrandAndCategoryRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
