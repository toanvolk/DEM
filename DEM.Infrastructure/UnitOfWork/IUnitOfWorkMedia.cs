
using DEM.EF;
using Microsoft.EntityFrameworkCore;
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
        DbSet<Category> Categories { get; }
        IRepositoryBase<Expense> ExpenseRepository { get; }
        DbSet<Expense> Expenses { get; }
        IRepositoryBase<Payer> PayerRepository { get; }
        DbSet<Payer> Payers { get; }
        IRepositoryBase<Intended> IntendedRepository { get; }
        DbSet<Intended> Intended { get; }
        IRepositoryBase<IntendedDetail> IntendedDetailRepository { get; }
        DbSet<IntendedDetail> IntendedDetail { get; }

        #endregion   

    }
}
