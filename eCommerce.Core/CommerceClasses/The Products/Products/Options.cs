using eCommerce.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Products
{
    //Untuk option variant (e.g Size, Warna, Ukuran)
    public class Options: EntityBase
    {
        public string OptionName { get; set; } = "";
        public long ProductId { get; set; } = 0;

        public virtual Product Product { get; set; }
        public virtual ICollection<OptionValue> OptionValue { get; set; }
    }
}
