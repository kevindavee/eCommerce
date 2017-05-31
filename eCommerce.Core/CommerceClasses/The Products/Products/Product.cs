﻿using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Products
{
    public class Product : EntityBase
    {
        public long CategoryId { get; set; } = 0;
        public string Nama { get; set; } = "";
        public string Deskripsi { get; set; } = "";
        public long BrandId { get; set; } = 0;
        public float Rating { get; set; } = 0;
        public long RatingCount { get; set; } = 0;

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }
}