using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss
{
    public class TransactionDetails : EntityBase
    {
        public long TransactionHeaderId { get; set; } = 0;
        public long ProductInstanceId { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Discount { get; set; } = 0;

        public virtual TransactionHeader TransactionHeader { get; set; }
        public virtual ProductInstance ProductInstance { get; set; }
    }
}
