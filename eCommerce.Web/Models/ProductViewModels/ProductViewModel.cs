﻿using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using eCommerce.Core.CommerceClasses.The_Products.Reviews;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.ProductViewModels
{
    public class ProductViewModel
    {
        public long CategoryId { get; set; }
        public List<ProductListViewModel> ProductList { get; set; }
        public List<Brand> BrandList { get; set; }
    }

    public class ProductListViewModel
    {
        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        public decimal Price { get; set; }
    }

    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public string ParentCategory { get; set; }
        public long ProductInstanceId { get; set; } = 0;


        public string selectedColor { get; set; }
        public List<string> colorList { get; set; }

        public string selectedUkuran { get; set; }
        public List<string> ukuranList { get; set; }

        public List<Review> Review { get; set; }
    }
}
