using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DEM.EF
{    
    [Table("IntendedDetail")]
    public class IntendedDetail: Entity
    {
        public Guid IntendedId { get; set; }
        public Guid CategoryId { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Money { get; set; }
    }
}
