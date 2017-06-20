using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.ShippingDetailss
{
    public class ShippingDetailsRepo : RepoBase<ShippingDetails>
    {
        public ShippingDetailsRepo(CommerceContext _context) : base(_context)
        {
        }

        /// <summary>
        /// Process Order
        /// </summary>
        /// <param name="TransactionHeaderId"></param>
        public void ProcessOrder(long TransactionHeaderId, string Username)
        {
            var shipping = dbSet.Where(s => s.TransactionHeaderId == TransactionHeaderId).FirstOrDefault();
            shipping.ShippingStatus = ShippingStatus.OrderProcessed;
            shipping.UpdatedBy = Username;
            shipping.UpdatedDate = DateTime.Today;
            Save(shipping);
        }

        public List<ShippingDetails> GetProcessedShippingDetails()
        {
            var list = dbSet.Where(s => s.ShippingStatus == ShippingStatus.OrderProcessed).ToList();

            return list;
        }

        public void UpdateTrackingNumber(long ShippingDetailId, string trackingNumber, string Username)
        {
            var shipping = GetById(ShippingDetailId);
            shipping.TrackingNumber = trackingNumber;
            shipping.ShippingStatus = ShippingStatus.Shipped;
            shipping.UpdatedDate = DateTime.Today;
            shipping.UpdatedBy = Username;
            Save(shipping);
        }
    }
}
