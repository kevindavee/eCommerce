using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.TransactionHeaders
{
    public class TransactionHeaderRepo : RepoBase<TransactionHeader>
    {
        public TransactionHeaderRepo(CommerceContext _context) : base(_context)
        {
        }

        /// <summary>
        /// Get customer's active cart. Return null if no active cart
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public TransactionHeader GetActiveCart(long CustomerId)
        {
            var cart = dbSet.Where(s => s.CurrentStatus == TransactionStatus.OnCart &&
                                         (s.LastStatus == TransactionStatus.OnCart || s.LastStatus == TransactionStatus.CheckedOut)  &&
                                         s.CustomerId == CustomerId)
                             .Include(i => i.TransactionDetails)
                             .FirstOrDefault();

            var checkedOut = dbSet.Where(s => s.CurrentStatus == TransactionStatus.CheckedOut &&
                                              s.LastStatus == TransactionStatus.OnCart &&
                                              s.CustomerId == CustomerId)
                                  .Include(i => i.TransactionDetails)
                                  .FirstOrDefault();

            if (cart == null)
            {
                if (checkedOut != null)
                {
                    cart = checkedOut;
                }
                else
                {
                    return null;
                }
            }

            return cart;
        }

        /// <summary>
        /// Change transaction header status and save it
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <param name="TransactionStatus"></param>
        /// <returns></returns>
        public void ChangeStatus(long TransactionId, string TransactionStatus, string UserName)
        {
            var transaction = GetById(TransactionId);

            transaction.LastStatus = transaction.CurrentStatus;
            transaction.CurrentStatus = TransactionStatus;

            transaction.UpdatedBy = UserName;
            transaction.UpdatedDate = DateTime.Today;

            Save(transaction);
        }

        /// <summary>
        /// Check if the transaction is waiting for payment confirmation. Return true if the state is waiting for payment confirmation
        /// </summary>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public bool IsWaitingForConfirmation(long TransactionId)
        {
            var result = GetById(TransactionId);
            if (result != null)
            {
                if (result.CurrentStatus == (TransactionStatus.PaymentConfirmation))
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Get transaction by id with details item, each item's product data and option value data
        /// </summary>
        /// <param name="TransactionHeaderId"></param>
        /// <returns></returns>
        public TransactionHeader GetTransactionFullDataById(long TransactionHeaderId)
        {
            var result = dbSet.Include(i => i.TransactionDetails).ThenInclude(j => j.ProductInstance.Product)
                              .Include(i => i.TransactionDetails).ThenInclude(j => j.ProductInstance.ProductInstanceOptions).ThenInclude(k => k.OptionValue.Options)
                              .FirstOrDefault(f => f.Id == TransactionHeaderId);
                              

            return result;
        }

        /// <summary>
        /// Get list of transactions to be approved by admin. This include details item, each item's Product, product instance options and option value. 
        /// </summary>
        /// <returns></returns>
        public List<TransactionHeader> GetTransactionsWaitingForApproval()
        {
            var result = dbSet.Where(s => s.CurrentStatus == TransactionStatus.PaymentConfirmation && s.LastStatus == TransactionStatus.CheckedOut)
                              .Include(i => i.TransactionDetails).ThenInclude(j => j.ProductInstance.Product)
                              .Include(i => i.TransactionDetails).ThenInclude(j => j.ProductInstance.ProductInstanceOptions).ThenInclude(k => k.OptionValue.Options)
                              .ToList();

            return result;
        }

        public string GenerateTransactionCode()
        {
            string code = "";
            var date = DateTime.Today.ToString("yyMMdd");
            var zero = "000000";
            var count = dbSet.Where(s => s.CreatedDate == DateTime.Today).Count();
            count++;
            zero = zero.Substring(count.ToString().Length);

            code = "TR" + date + zero + count.ToString();
            return code;
        }

    }
}
