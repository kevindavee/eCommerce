using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Stocks
{
    public class Stock: EntityBase
    {
        public long ProductInstanceId { get; set; }
        public int Quantity { get; set; } = 0;
        public int OnCart { get; set; } = 0;

        public virtual ProductInstance ProductInstance { get; set; }
    }
}
