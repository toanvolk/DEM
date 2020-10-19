using DEM.EF;
using System;
using System.Collections.Generic;
namespace DEM.App
{
    public interface IExpenseService
    {
        bool Create(List<ExpenseDto> expenses);
        Tuple<List<ExpenseDto>, int> LoadData(RootCategoryEnum rootCategoryType, DateTime startTime, DateTime endTime, int page, int pageSize);
        ICollection<Payer> GetPayers();
        ExpenseDto GetData(Guid expenseId);
        bool Update(ExpenseDto data);
        bool Delete(Guid expenseId);
    }
}
