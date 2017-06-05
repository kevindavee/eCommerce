using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Products
{
    public class ProductInstance: EntityBase
    {
        public long ProductId { get; set; }
        public int Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProductInstanceOptions> ProductInstanceOptions { get; set; }
        public virtual ICollection<TransactionDetails> TransactionDetails { get; set; }
    }
}
