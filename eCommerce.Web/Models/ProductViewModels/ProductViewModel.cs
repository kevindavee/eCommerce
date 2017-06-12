using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public List<ProductListViewModel> ProductList { get; set; }
        public List<Brand> BrandList { get; set; }
    }

    public class ProductListViewModel
    {
        public Product Product { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
    }
}
