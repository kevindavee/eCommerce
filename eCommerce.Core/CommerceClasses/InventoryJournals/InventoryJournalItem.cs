using eCommerce.Commons;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Core.CommerceClasses.InventoryJournals
{
    public class InventoryJournalItem: EntityBase
    {
        public long InventoryJournalId { get; set; }
        public long ProductInstanceId { get; set; }
        public int Quantity { get; set; } = 0;

        public virtual InventoryJournal InventoryJournal { get; set; }
        public virtual ProductInstance ProductInstance { get; set; }
    }
}
