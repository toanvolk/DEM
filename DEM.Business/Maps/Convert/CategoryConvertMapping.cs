using AutoMapper;
using DEM.EF;
using System;
using System.Collections.Generic;

namespace DEM.App
{
    public class CategoryConvertMapping :
        ITypeConverter<CategoryDto, Category>,
        ITypeConverter<List<Category>, List<CategoryDto>>
    {
        //Create
        public Category Convert(CategoryDto source, Category destination, ResolutionContext context)
        {
            destination ??= new Category();
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Type = Enum.GetName(typeof(RootCategoryEnum), source.Type);
            destination.CreatedDate = DateTime.Now;
            destination.CreatedBy = "ADMIN";

            return destination;
        }
        //Load
        public List<CategoryDto> Convert(List<Category> source, List<CategoryDto> destination, ResolutionContext context)
        {
            destination ??= new List<CategoryDto>();
            foreach (var item in source)
            {
                destination.Add(new CategoryDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Type = Enum.Parse<RootCategoryEnum>(item.Type)
                }) ;
            }

            return destination;
        }
    }
}
