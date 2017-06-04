using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans
{
    public class KonfirmasiPembayaranRepo : RepoBase<KonfirmasiPembayaran>
    {
        public KonfirmasiPembayaranRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
