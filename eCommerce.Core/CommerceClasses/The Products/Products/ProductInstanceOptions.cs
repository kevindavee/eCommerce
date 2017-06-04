using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Products
{
    public class ProductInstanceOptions
    {
        [Key]
        [Column(Order = 0)]
        public long ProductInstanceId { get; set; }
        [Key]
        [Column(Order = 1)]
        public long OptionValueId { get; set; }

        public virtual ProductInstance ProductInstance { get; set; }
        public virtual OptionValue OptionValue { get; set; }
    }
}
