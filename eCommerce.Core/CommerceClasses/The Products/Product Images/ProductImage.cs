using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Product_Images
{
    public class ProductImage : EntityBase
    {
        public long ProductId { get; set; } = 0;
        public string Path { get; set; } = "";

        public virtual Product Product { get; set; }
    }
}
