using eCommerce.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.InventoryJournals
{
    public class InventoryJournal: EntityBase
    {
        public string JournalCode { get; set; } = "";
        public string JournalType { get; set; } = "";
        public string Remarks { get; set; } = "";

        public ICollection<InventoryJournalItem> InventoryJournalItem { get; set; }
    }
}
