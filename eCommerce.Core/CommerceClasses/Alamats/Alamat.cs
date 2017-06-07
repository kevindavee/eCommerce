using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Alamats
{
    public class Alamat : EntityBase
    {
        public long CustomerId { get; set; } = 0;
        public string NamaAlamat { get; set; } = "";
        public string TheAlamat { get; set; } = "";
        public string Kota { get; set; } = "";
        public string Provinsi { get; set; } = "";
        public int KodePos { get; set; } = 0;

        public virtual Customer Customer { get; set; }
    }
}
