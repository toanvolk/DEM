using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DEM.EF
{
    [Table("Payer")]
    public class Payer : AuditableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
