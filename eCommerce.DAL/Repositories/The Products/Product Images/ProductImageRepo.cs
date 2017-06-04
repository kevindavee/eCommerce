using eCommerce.Core.CommerceClasses.The_Products.Product_Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.The_Products.Product_Images
{
    public class ProductImageRepo : RepoBase<ProductImage>
    {
        public ProductImageRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
