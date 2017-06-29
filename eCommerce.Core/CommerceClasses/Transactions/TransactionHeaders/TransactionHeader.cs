using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Customers;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders
{
    public class TransactionHeader : EntityBase
    {
        public string Code { get; set; } = "";
        public DateTime TglTransaksi { get; set; } = DateTime.Today;
        public long CustomerId { get; set; } = 0;
        public string LastStatus { get; set; } = "";
        public string CurrentStatus { get; set; } = "";

        [DisplayFormat(DataFormatString = "{0:#,##0.##}")]
        public decimal TotalPrice { get; set; } = 0;
        public decimal TotalDiscount { get; set; } = 0;
        public string Remarks { get; set; } = "";

        public virtual Customer Customer { get; set; }
        public virtual ShippingDetails ShippingDetails { get; set; }
        public virtual ICollection<TransactionDetails> TransactionDetails { get; set; }
    }
}
