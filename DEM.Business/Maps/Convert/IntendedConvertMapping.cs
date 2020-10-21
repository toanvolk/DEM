using AutoMapper;
using DEM.EF;
using System;
using System.Collections.Generic;

namespace DEM.App
{
    public class IntendedConvertMapping :
        ITypeConverter<IntendedDto, Intended>,
        ITypeConverter<List<IntendedDetailDto>, List<IntendedDetail>>,
        ITypeConverter<Intended, IntendedDto>,
        ITypeConverter<List<IntendedDetail>, List<IntendedDetailDto>>
    {
        public Intended Convert(IntendedDto source, Intended destination, ResolutionContext context)
        {
            destination ??= new Intended();
            destination.Id = source.Id;
            destination.RootCategory = source.RootCategory;
            destination.Description = source.Description;
            destination.FromDate = source.FromDate;
            destination.ToDate = source.ToDate;
            destination.CreatedBy = "ADMIN";
            destination.CreatedDate = DateTime.Now;

            return destination;
        }

        public IntendedDetail Convert(IntendedDetailDto source, IntendedDetail destination, ResolutionContext context)
        {
            destination ??= new IntendedDetail();
            destination.IntendedId = source.IntendedId;
            destination.CategoryId = source.CategoryId;
            destination.Money = source.Money;

            return destination;
        }

        public List<IntendedDetail> Convert(List<IntendedDetailDto> source, List<IntendedDetail> destination, ResolutionContext context)
        {
            destination ??= new List<IntendedDetail>();
            foreach (var item in source)
            {
                destination.Add(new IntendedDetail()
                {
                    CategoryId = item.CategoryId,
                    IntendedId = item.IntendedId,
                    Money = item.Money
                });
            }
            return destination;
        }

        public IntendedDto Convert(Intended source, IntendedDto destination, ResolutionContext context)
        {
            destination ??= new IntendedDto();
            destination.Id = source.Id;
            destination.Description = source.Description;
            destination.FromDate = source.FromDate;
            destination.ToDate = source.ToDate;
            destination.RootCategory = source.RootCategory;
            return destination;
        }

        public List<IntendedDetailDto> Convert(List<IntendedDetail> source, List<IntendedDetailDto> destination, ResolutionContext context)
        {
            destination ??= new List<IntendedDetailDto>();
            foreach (var item in source)
            {
                destination.Add(new IntendedDetailDto() { 
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    IntendedId = item.IntendedId,
                    Money = item.Money
                });
            }
            return destination;
        }
    }
}
