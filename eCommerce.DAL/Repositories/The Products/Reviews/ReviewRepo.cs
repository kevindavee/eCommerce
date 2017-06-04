using eCommerce.Core.CommerceClasses.The_Products.Reviews;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Reviews
{
    public class ReviewRepo : RepoBase<Review>
    {
        public ReviewRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
