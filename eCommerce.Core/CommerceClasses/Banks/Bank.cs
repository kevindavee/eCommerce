using eCommerce.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Banks
{
    public class Bank : EntityBase
    {
        public string Nama { get; set; } = "";
        public long AccountNumber { get; set; } = 0;
        public string AccountHolder { get; set; } = "";
    }
}
