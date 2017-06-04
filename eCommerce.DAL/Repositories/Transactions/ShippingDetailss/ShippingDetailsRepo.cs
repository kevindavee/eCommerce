using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Transactions.ShippingDetailss
{
    public class ShippingDetailsRepo : RepoBase<ShippingDetails>
    {
        public ShippingDetailsRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
