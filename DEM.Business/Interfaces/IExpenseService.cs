using System.Collections.Generic;
namespace DEM.App
{
    public interface IExpenseService
    {
        bool Create(List<ExpenseDto> expenses);
    }
}
