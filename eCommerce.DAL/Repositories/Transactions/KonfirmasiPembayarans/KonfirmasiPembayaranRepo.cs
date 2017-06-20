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
            return dbSet.Where(s => s.IsValid == null)
                        .Include(i => i.TransactionHeader)
                        .Include(i => i.Bank).ToList();
        }

        public void Validate(long KonfirmasiPembayaranId, bool isValid, string Username)
        {
            var item = GetById(KonfirmasiPembayaranId);
            item.IsValid = isValid;
            item.UpdatedBy = Username;
            item.UpdatedDate = DateTime.Today;
            Save(item);
        }
        /// <summary>
        /// Calculate all validated transfer amount from payment confirmation
        /// </summary>
        /// <param name="TransactionHeaderId"></param>
        /// <returns></returns>
        public decimal GetAllTransferedAmountByHeaderId(long TransactionHeaderId)
        {
            decimal total = 0;
            var list = dbSet.Where(s => s.TransactionHeaderId == TransactionHeaderId && s.IsValid == true).ToList();

            foreach (var item in list)
            {
                total += item.NominalTransfer;
            }

            return total;
        }
    }
}
