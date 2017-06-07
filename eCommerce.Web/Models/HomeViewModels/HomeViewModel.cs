using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public List<Product> ProductList { get; set; }
        public List<Brand> BrandList { get; set; }
    }
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
    }
}
