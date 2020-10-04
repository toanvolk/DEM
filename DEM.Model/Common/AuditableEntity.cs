using System;
using System.ComponentModel.DataAnnotations;

namespace DEM.EF
{
    public class AuditableEntity : Entity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [MaxLength(20)]
        public string CreatedBy { get; set; }
        [MaxLength(20)]
        public string UpdatedBy { get; set; }
    }
}
