using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.UserLogins;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Customers
{
    public class Customer : EntityBase
    {
        public string Nama { get; set; } = "";
        public bool JenisKelamin { get; set; } = true;
        public bool StatusNikah { get; set; } = true;
        public string Pekerjaan { get; set; } = "";
        public DateTime TanggalLahir { get; set; } = DateTime.Today;
        public string NoTelepon { get; set; } = "";
        public string Email { get; set; } = "";
        public string Foto { get; set; } = "";

        public virtual UserLogin UserLogin { get; set; }
    }
}
