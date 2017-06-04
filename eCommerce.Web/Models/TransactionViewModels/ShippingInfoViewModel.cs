﻿using eCommerce.Core.CommerceClasses.Alamats;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.TransactionViewModels
{
    public class ShippingInfoViewModel
    {
        public TransactionHeader Transaction { get; set; }
        public List<Alamat> Alamat { get; set; }
    }
}
