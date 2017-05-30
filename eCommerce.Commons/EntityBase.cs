using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Commons
{
    public class EntityBase
    {
        public long Id { get; set; } = 0;
        public string CreatedBy { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        public string UpdatedBy { get; set; } = "";
        public DateTime UpdatedDate { get; set; } = DateTime.Today;
    }
}
