using eCommerce.Core.CommerceClasses.Shippers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.Shippers
{
    public class ShipperRepo : RepoBase<Shipper>
    {
        public ShipperRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
