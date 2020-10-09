using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface ICategoryService
    {
        List<CategoryDto> LoadDatas(string rootCategoryType, bool isAll);
        bool Add(CategoryDto categoryDto);
        bool Edit(CategoryDto categoryDto);
        bool Delete(Guid id);
        CategoryDto FindId(Guid id);
        bool ChangeStatu(Guid categoryId, bool notUse);
    }
}
