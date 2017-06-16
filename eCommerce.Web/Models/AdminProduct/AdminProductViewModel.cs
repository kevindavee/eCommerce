using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.AdminProduct
{
    public class AdminProductViewModel
    {
    }
    public class IndexPageAdminProductViewModel
    {
        public List<Product> listProduct { get; set; }
        public DetailsProductViewModel DetailsProduct { get; set; } = new DetailsProductViewModel();
    }

    public class DetailsProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public List<Category> listCategory { get; set; } = new List<Category>();
        public List<Category> listSubCategory { get; set; } = new List<Category>();
        public List<Brand> listBrand { get; set; } = new List<Brand>();
    }
}
