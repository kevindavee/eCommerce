using eCommerce.Core.CommerceClasses.The_Products.Reviews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Reviews
{
    public class ReviewRepo : RepoBase<Review>
    {
        public ReviewRepo(CommerceContext _context) : base(_context)
        {
            
        }
        public List<Review> GetListReviewByProductId(long productId)
        {
            var list = new List<Review>();
            list = dbSet.Where(j => j.ProductId == productId)
                            .Include(j => j.Customer)
                            .ToList();
            return list;
        }
    }
}
