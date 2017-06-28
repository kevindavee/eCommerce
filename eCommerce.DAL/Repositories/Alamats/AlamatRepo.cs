using eCommerce.Core.CommerceClasses.Alamats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.Alamats
{
    public class AlamatRepo : RepoBase<Alamat>
    {
        public AlamatRepo(CommerceContext _context): base(_context)
        {
        }

        public List<Alamat> GetAlamatForCurrentCustomer(long CustomerId)
        {
            return dbSet.Where(s => s.CustomerId == CustomerId && !s.Deleted).ToList();
        }
    }
}
