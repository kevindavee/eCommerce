using eCommerce.Core.CommerceClasses.Brands;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.KumpulanRepos.Brands
{
    public class BrandRepo : RepoBase<Brand>
    {
        public BrandRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
