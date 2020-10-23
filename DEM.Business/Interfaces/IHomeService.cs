using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IHomeService : IBaseService
    {
        Tuple<ICollection<string>, List<List<decimal>>> GetDailyInMonthCurrent();
        Tuple<ICollection<string>, List<List<decimal>>> GetExpenseRealAndIntended();
    }
}
