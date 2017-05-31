using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.BrandsAndCategories
{
    public class BrandAndCategory : EntityBase
    {
        public long BrandId { get; set; } = 0;
        public long CategoryId { get; set; } = 0;

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
