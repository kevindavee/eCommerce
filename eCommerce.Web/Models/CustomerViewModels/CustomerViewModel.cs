using eCommerce.Core.CommerceClasses.Alamats;
using eCommerce.Core.CommerceClasses.Customers;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.CustomerViewModels
{
    public class CustomerViewModel
    {
    }

    public class ProfileViewModel
    {
        public Customer Customer { get; set; }
        public string JobLainnya { get; set; } = "";
        public int Day { get; set; } 
        public int Month { get; set; }
        public int Year { get; set; } 

    }
    public class DaftarAlamatViewModel
    {
        public List<Alamat> ListAlamat { get; set; }
        public long CustomerId { get; set; }
    }

    public class AlamatViewModel
    {
        public Alamat Alamat { get; set; }
        public long CustomerId { get; set; }
    }

    public class TransactionHistoryViewModel
    {
        public List<TransactionHeader> ListTransaction { get; set; } = new List<TransactionHeader>();
    }

    public class DetailsTransactionHistoryViewModel
    {
        public TransactionHeader TransactionHeader { get; set; }
    }
}
