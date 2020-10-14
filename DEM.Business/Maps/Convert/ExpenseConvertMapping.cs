using AutoMapper;
using DEM.EF;
using System;
using System.Collections.Generic;

namespace DEM.App
{
    public class ExpenseConvertMapping :
        ITypeConverter<List<ExpenseDto>, List<Expense>>
    {
        //Create
        public List<Expense> Convert(List<ExpenseDto> source, List<Expense> destination, ResolutionContext context)
        {
            destination ??= new List<Expense>();
            foreach (var item in source)
            {
                destination.Add(new Expense() { 
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    Money = item.Money,
                    Payer = item.Payer,
                    PayTime = item.PayTime,
                    CreatedBy = "ADMIN",
                    CreatedDate = DateTime.Now                    
                });
            }          
            return destination;
        }

    }
}
