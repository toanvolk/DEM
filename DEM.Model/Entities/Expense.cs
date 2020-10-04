using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEM.EF
{
    [Table("Expense")]
    public class Expense : AuditableEntity
    {
        public string Description { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Money { get; set; }
        [MaxLength(100)]
        public string Payer { get; set; }
        public DateTime PayTime { get; set; }
    }
}
