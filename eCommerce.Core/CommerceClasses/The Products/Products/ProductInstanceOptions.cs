using eCommerce.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Products
{
    public class ProductInstanceOptions
    {
        public long ProductInstanceId { get; set; }   
        public long OptionValueId { get; set; }

        public virtual ProductInstance ProductInstance { get; set; }
        public virtual OptionValue OptionValue { get; set; }
    }
}
