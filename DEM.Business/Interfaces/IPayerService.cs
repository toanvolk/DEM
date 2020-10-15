using DEM.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IPayerService
    {
        ICollection<Payer> getData();
    }
}
