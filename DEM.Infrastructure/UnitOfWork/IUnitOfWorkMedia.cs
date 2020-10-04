
using DEM.EF;
using System.Threading.Tasks;

namespace DEM.Infrastructure
{
    public interface IUnitOfWorkMedia
    {
        //MediaContext dbContext { get; }
        int SaveChanges();

        Task<int> SaveChangesAsync();

        int ExecQueryCommand(string sqlQuery, params object[] param);

        Task<int> ExecQueryCommandAsync(string sqlQuery, params object[] param);

        #region Implement Repository
        IRepositoryBase<Category> CategoryRepository { get; }
        IRepositoryBase<Expense> ExpenseRepository { get; }
        #endregion   
    }
}
