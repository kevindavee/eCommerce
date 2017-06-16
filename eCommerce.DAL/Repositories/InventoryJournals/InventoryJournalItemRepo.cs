using eCommerce.Core.CommerceClasses.InventoryJournals;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Repositories.InventoryJournals
{
    public class InventoryJournalItemRepo : RepoBase<InventoryJournalItem>
    {
        public InventoryJournalItemRepo(CommerceContext _context) : base(_context)
        {
        }
    }
}
