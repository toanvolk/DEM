using AutoMapper;
using DEM.EF;
using System.Collections.Generic;

namespace DEM.App
{
    public class CategoryProfileMapping : Profile
    {
        public CategoryProfileMapping()
        {
            CreateMap<CategoryDto, Category>().ConvertUsing<CategoryConvertMapping>();
            CreateMap<Category, CategoryDto>().ConvertUsing<CategoryConvertMapping>();
            CreateMap<List<Category>, List<CategoryDto>>().ConvertUsing<CategoryConvertMapping>();
        }
    }
}
