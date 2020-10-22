using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IHomeService : IBaseService
    {
        Tuple<ICollection<string>, ICollection<string>> GetDailyInMonthCurrent();
    }
}
