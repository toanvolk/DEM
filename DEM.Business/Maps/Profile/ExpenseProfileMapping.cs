using AutoMapper;
using DEM.EF;
using System.Collections.Generic;

namespace DEM.App
{
    public class ExpenseProfileMapping : Profile
    {
        public ExpenseProfileMapping()
        {
            CreateMap<List<ExpenseDto>, List<Expense>>().ConvertUsing<ExpenseConvertMapping>();
        }
    }
}
