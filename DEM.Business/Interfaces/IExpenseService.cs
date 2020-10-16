using DEM.EF;
using System;
using System.Collections.Generic;
namespace DEM.App
{
    public interface IExpenseService
    {
        bool Create(List<ExpenseDto> expenses);
        List<ExpenseDto> LoadData(Guid categoryId, DateTime startTime, DateTime endTime);
        ICollection<Payer> GetPayers();
        ExpenseDto GetData(Guid expenseId);
        bool Update(ExpenseDto data);
        bool Delete(Guid expenseId);
    }
}
