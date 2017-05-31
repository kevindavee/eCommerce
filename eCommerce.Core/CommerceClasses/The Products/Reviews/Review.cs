using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Customers;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.The_Products.Reviews
{
    public class Review : EntityBase
    {
        public long ProductId { get; set; } = 0;
        public long CustomerId { get; set; } = 0;
        public DateTime TanggalReview { get; set; } = DateTime.Today;
        public string TheReview { get; set; } = "";

        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
