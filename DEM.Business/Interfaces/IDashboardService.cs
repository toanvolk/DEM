using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IDashboardService
    {
        ICollection<DailyInMonthCurrentDto> GetDailyInMonthCurrent();
    }
}
