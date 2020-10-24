using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DEM.EF
{    
    [Table("Intended")]
    public class Intended : AuditableEntity
    {        
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string RootCategory { get; set; }
    }
}
