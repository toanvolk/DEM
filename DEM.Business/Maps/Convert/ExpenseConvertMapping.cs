using AutoMapper;
using DEM.EF;
using System;
using System.Collections.Generic;

namespace DEM.App
{
    public class ExpenseConvertMapping :
        ITypeConverter<List<ExpenseDto>, List<Expense>>,
        ITypeConverter<Expense, ExpenseDto>,
        ITypeConverter<ExpenseDto, Expense>
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
        //Fill data
        public ExpenseDto Convert(Expense source, ExpenseDto destination, ResolutionContext context)
        {
            destination = destination ?? new ExpenseDto();
            destination.Id = source.Id;
            destination.CategoryId = source.CategoryId;
            destination.Description = source.Description;
            destination.Money = source.Money;
            destination.Payer = source.Payer;
            destination.PayTime = source.PayTime;

            return destination;
        }
        //Update
        public Expense Convert(ExpenseDto source, Expense destination, ResolutionContext context)
        {
            destination = destination ?? new Expense();
            destination.Id = source.Id;
            destination.CategoryId = source.CategoryId;
            destination.Payer = source.Payer;
            destination.PayTime = source.PayTime;
            destination.Money = source.Money;
            destination.Description = source.Description;

            destination.UpdatedBy = "ADMIN";
            destination.UpdatedDate = DateTime.Now;
            return destination;
        }
    }
}
