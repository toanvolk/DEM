using DEM.EF;
using System;
using System.Collections.Generic;
namespace DEM.App
{
    public interface IExpenseService
    {
        bool Create(List<ExpenseDto> expenses);
        List<ExpenseDto> LoadData(Guid categoryId);
        ICollection<Payer> GetPayers();
    }
}
