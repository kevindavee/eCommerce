using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans
{
    public class KonfirmasiPembayaranRepo : RepoBase<KonfirmasiPembayaran>
    {
        public KonfirmasiPembayaranRepo(CommerceContext _context) : base(_context)
        {
        }

        public List<KonfirmasiPembayaran> GetActiveList()
        {
            var result = dbSet.Where(s => s.TransactionHeader.CurrentStatus == TransactionStatus.PaymentConfirmation)
                              .Include(i => i.Bank)
                              .ToList();

            return result;
        }
    }
}
