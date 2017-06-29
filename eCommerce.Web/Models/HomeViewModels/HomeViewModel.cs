using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public List<Product> Product1 { get; set; }
        public List<Product> Product2 { get; set; }
        public List<Product> Product3 { get; set; }
    }
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
    }
}
