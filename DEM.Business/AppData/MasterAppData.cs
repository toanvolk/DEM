using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class MasterAppData
    {
        public List<Payer> Payers { get; } = new List<Payer>()
        {
            new Payer()
            {
                Code = "VK",
                Name = "Vợ"
            },
            new Payer()
            {
                Code = "CK",
                Name = "Chồng"
            }
        };
    }
    public class Payer
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
