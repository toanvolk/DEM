﻿using AutoMapper;
using DEM.EF;
using System.Collections.Generic;

namespace DEM.App
{
    public class IntendedProfileMapping : Profile
    {
        public IntendedProfileMapping()
        {
            //Create
            CreateMap<IntendedDto, Intended>().ConvertUsing<IntendedConvertMapping>();
            CreateMap<List<IntendedDetailDto>, List<IntendedDetail>>().ConvertUsing<IntendedConvertMapping>();            
        }
    }
}
