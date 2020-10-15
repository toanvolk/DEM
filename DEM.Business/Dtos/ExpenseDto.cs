using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class ExpenseDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Money { get; set; }
        public string Payer { get; set; }
        public string PayerName { get; set; }
        public DateTime PayTime { get; set; }
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
