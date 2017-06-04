using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCommerce.Commons
{
    public class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } = 0;
        public string CreatedBy { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        public string UpdatedBy { get; set; } = "";
        public DateTime UpdatedDate { get; set; } = DateTime.Today;
    }
}
