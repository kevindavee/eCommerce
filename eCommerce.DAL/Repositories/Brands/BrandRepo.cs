using eCommerce.Core.CommerceClasses.Brands;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Brands
{
    public class BrandRepo : RepoBase<Brand>
    {
        public BrandRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
