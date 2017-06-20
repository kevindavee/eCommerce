using eCommerce.Core.CommerceClasses.Alamats;
using eCommerce.Core.CommerceClasses.Customers;
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
    }
    public class DaftarAlamatViewModel
    {
        public List<Alamat> ListAlamat { get; set; }
    }

    public class AlamatViewModel
    {
        public Alamat Alamat { get; set; }
        public long CustomerId { get; set; }
    }
}
