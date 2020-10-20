using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public interface IIntendedService
    {
        ICollection<CategoryDto> GetCategories(string roostCategory);
        bool Create(IntendedDto intendedDto);
    }
}
