
using DEM.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DEM.Infrastructure
{
    public class UnitOfWorkMedia : IUnitOfWorkMedia
    {
        //  var context = services.GetRequiredService<DEMContext>();
        //  public DEMContext dbContext =>  new DEMContext;
        private DEMContext _dbContext;
        public UnitOfWorkMedia(DEMContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region method
        public int SaveChanges()
        {
            CheckIsDisposed();
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            CheckIsDisposed();
            return _dbContext.SaveChangesAsync();
        }

        private bool _disposed = false;
        private void CheckIsDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        [Obsolete]
        public int ExecQueryCommand(string sqlQuery, params object[] param) => _dbContext.Database.ExecuteSqlCommand(sqlQuery, param); //"procedureName @p0, @p1", parameters: new[] { "Bill", "Gates" }

        [Obsolete]
        public Task<int> ExecQueryCommandAsync(string sqlQuery, params object[] param) => _dbContext.Database.ExecuteSqlCommandAsync(sqlQuery, param); //"CreateTable @p0, @p1", parameters: new[]
        #endregion end method

        #region register reponsitory
        private IRepositoryBase<Category> _categoryRepository;
        public IRepositoryBase<Category> CategoryRepository => _categoryRepository ?? (_categoryRepository = new RepositoryBase<Category>(_dbContext));
        private IRepositoryBase<Expense> _expenseRepository;
        public IRepositoryBase<Expense> ExpenseRepository => _expenseRepository ?? (_expenseRepository = new RepositoryBase<Expense>(_dbContext));
        private IRepositoryBase<Payer> _payerRepository;
        public IRepositoryBase<Payer> PayerRepository => _payerRepository ?? (_payerRepository = new RepositoryBase<Payer>(_dbContext));
        public DbSet<Category> Categories => _dbContext.Categorys;
        public DbSet<Expense> Expenses => _dbContext.Expenses;
        public DbSet<Payer> Payers => _dbContext.Payers;

        #endregion end register reponsitory
    }
}
