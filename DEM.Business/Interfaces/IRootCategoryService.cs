using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IRootCategoryService: IBaseService
    {
        List<RootCategoryDto> GetDatas();
    }
}
