using eCommerce.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Products
{
    //Untuk value dari suatu option (e.g Size: 5, Warna: Red, Ukuran: L)
    public class OptionValue: EntityBase
    {
        public long OptionsId { get; set; }
        public string Value { get; set; }

        public virtual Options Options { get; set; }
        public virtual ICollection<ProductInstanceOptions> ProductInstanceOptions { get; set; }
    }
}
