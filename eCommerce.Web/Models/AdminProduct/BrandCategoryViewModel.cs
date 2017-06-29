using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.AdminProduct
{
    public class BrandCategoryViewModel
    {
        public Brand Brand { get; set; }
        public List<Category> Categories { get; set; }
    }
}
