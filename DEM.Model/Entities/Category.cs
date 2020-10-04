using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEM.EF
{
    [Table("Category")]
    public class Category : AuditableEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public bool? NotUse { get; set; }
    }
}
