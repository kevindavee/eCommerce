using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.AdminTransaction
{
    public class ManageShipmentViewModel
    {
        public List<ShippingDetails> ShippingDetails { get; set; }
    }
}
