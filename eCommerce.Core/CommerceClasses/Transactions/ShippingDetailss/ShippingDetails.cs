using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Shippers;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss
{
    public class ShippingDetails : EntityBase
    {
        public long TransactionHeaderId { get; set; } = 0;
        public string NamaPenerima { get; set; } = "";
        public string AlamatPengiriman { get; set; } = "";
        public long ShipperId { get; set; } = 0;
        public string TrackingNumber { get; set; } = "";

        public virtual TransactionHeader TransactionHeader { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
