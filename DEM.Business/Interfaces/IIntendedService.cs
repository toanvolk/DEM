using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IIntendedService
    {
        ICollection<CategoryDto> GetCategories(string roostCategory);
        bool Create(IntendedDto intendedDto);
        Tuple<ICollection<IntendedDto>, int> LoadData(string rootCategory, DateTime startTime, DateTime endTime, int page, int pageSize);
        IntendedDto GetData(Guid intendedId);
        bool Update(IntendedDto data);
        bool Delete(Guid id);
    }
}
