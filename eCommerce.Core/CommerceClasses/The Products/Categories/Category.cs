using eCommerce.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Categories
{
    public class Category : EntityBase
    {
        public string Nama { get; set; } = "";
        public long ParentId { get; set; } = 0;
    }
}
