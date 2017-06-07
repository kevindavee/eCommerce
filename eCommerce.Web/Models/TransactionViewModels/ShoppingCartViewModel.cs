using eCommerce.Core.CommerceClasses.The_Products.Products;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.TransactionViewModels
{
    public class ShoppingCartViewModel
    {
        public TransactionHeader TransactionHeader { get; set; }
        public List<ProductInstanceOptions> ProductInstanceOptions { get; set; }
    }
}
