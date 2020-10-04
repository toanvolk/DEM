using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RootCategoryEnum Type { get; set; }
        public bool NotUse { get; set; }
    }
}
