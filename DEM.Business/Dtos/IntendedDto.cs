using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class IntendedDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string RootCategory { get; set; }
        public List<IntendedDetailDto> Details { get; set; }
    }
    public class IntendedDetailDto
    {
        public Guid Id { get; set; }
        public Guid IntendedId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Money { get; set; }
    }
}
