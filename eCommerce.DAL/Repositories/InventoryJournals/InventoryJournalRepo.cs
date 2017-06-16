using eCommerce.Core.CommerceClasses.InventoryJournals;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.InventoryJournals
{
    public class InventoryJournalRepo : RepoBase<InventoryJournal>
    {
        public InventoryJournalRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
