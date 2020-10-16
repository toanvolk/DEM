using AutoMapper;
using DEM.EF;
using System.Collections.Generic;

namespace DEM.App
{
    public class ExpenseProfileMapping : Profile
    {
        public ExpenseProfileMapping()
        {
            //Create
            CreateMap<List<ExpenseDto>, List<Expense>>().ConvertUsing<ExpenseConvertMapping>();
            //Fill data 
            CreateMap<Expense, ExpenseDto>().ConvertUsing<ExpenseConvertMapping>();
            //Update
            CreateMap<ExpenseDto, Expense>().ConvertUsing<ExpenseConvertMapping>();
        }
    }
}
