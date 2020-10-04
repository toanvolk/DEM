using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface ICategoryService
    {
        List<CategoryDto> LoadDatas(string rootCategoryType);
        bool Add(CategoryDto categoryDto);
        bool Edit(CategoryDto categoryDto);
        bool Delete(Guid id);
    }
}
