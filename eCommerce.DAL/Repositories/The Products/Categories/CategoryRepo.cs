
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Categories
{
    public class CategoryRepo : RepoBase<Category>
    {
        public CategoryRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
