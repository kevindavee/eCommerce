using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Banks;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans
{
    public class KonfirmasiPembayaran : EntityBase
    {
        public long TransactionHeaderId { get; set; } = 0;
        public long BankId { get; set; } = 0;
        public string NoRekening { get; set; } = "";
        public string NamaPemilikRekening { get; set; } = "";
        public decimal NominalTransfer { get; set; } = 0;
        public bool? IsValid { get; set; } = null;
        public string ImageBuktiTransfer { get; set; } = "";

        public virtual TransactionHeader TransactionHeader { get; set; }
        public virtual Bank Bank { get; set; }
    }
}
