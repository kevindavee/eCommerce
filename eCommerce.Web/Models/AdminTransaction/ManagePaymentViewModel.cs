using eCommerce.Core.CommerceClasses.The_Products.Products;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.AdminTransaction
{
    public class ManagePaymentViewModel
    {
        public List<TransactionHeader> transactionHeader { get; set; }
        public List<KonfirmasiPembayaran> konfirmasiPembayaran { get; set; }
    }
}
