using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.Stocks;
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
        public ProductStockPriceViewModel StockPriceProduct { get; set; } = new ProductStockPriceViewModel();
    }

    public class DetailsProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public long CategoryId { get; set; } = 0;
        public decimal DefaultPrice { get; set; } = 0;
        public List<Category> listCategory { get; set; } = new List<Category>();
        public List<Category> listSubCategory { get; set; } = new List<Category>();
        public List<OptionListViewModel> listOptions { get; set; } = new List<OptionListViewModel>();
        public List<Brand> listBrand { get; set; } = new List<Brand>();
        public List<ListProductOption> listProductOption { get; set; } = new List<ListProductOption>();
    }

    public class ListProductOption
    {
        public long Id { get; set; }
        public string Nama { get; set; } = "";
    }
    public class OptionListViewModel
    {
        public Options Options { get; set; } = new Options();
        public bool Selected { get; set; } = false;
    }

    public class ProductStockPriceViewModel
    {
        public Product Product { get; set; } = new Product();
        public List<StockPriceModel> ListProductInt { get; set; } = new List<StockPriceModel>();
    }
    public class StockPriceModel
    {
        public string Nama { get; set; } = "";
        public Stock Stock { get; set; } = new Stock();
        public ProductInstance ProductInstance { get; set; } = new ProductInstance();
    }
}
