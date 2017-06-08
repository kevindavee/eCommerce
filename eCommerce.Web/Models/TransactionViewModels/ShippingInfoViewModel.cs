using eCommerce.Core.CommerceClasses.Alamats;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.TransactionViewModels
{
    public class CheckOutViewModel
    {
        public CheckOutViewModel()
        {
            ShippingDetail = new ShippingDetails();
        }

        public TransactionHeader Transaction { get; set; }
        public ShippingDetails ShippingDetail { get; set; }
        public Alamat Alamat { get; set; }
    }
}
