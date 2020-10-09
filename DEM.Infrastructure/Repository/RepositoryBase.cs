
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DEM.Infrastructure
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private DbSet<TEntity> _dbSet;
       
        public RepositoryBase(DbContext dbcontext)
        {            
            _dbContext = dbcontext;
            _dbSet = dbcontext.Set<TEntity>();
        }

        public virtual int Count => _dbSet.Count();

        public virtual decimal Sum(Expression<Func<TEntity, decimal?>> condition)
        {
            return _dbSet.Sum(condition) ?? 0;
        }

        public bool Any(object primaryKey)
        {
            return _dbSet.Find(primaryKey) == null ? false : true;
        }
        public virtual TEntity FindById(Guid id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, string condition = "")
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            foreach (string includeProperty in condition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> condition) => _dbSet.Where(condition);

        public virtual IQueryable<TEntity> GetAllData(Expression<Func<TEntity, bool>> condition, int currentPage, int pageSize, Expression<Func<TEntity, string>> orderby)
            => _dbSet.Where(condition).OrderBy(orderby).ThenBy(orderby).Skip((currentPage - 1) * pageSize).Take(pageSize);

        public virtual void Add(TEntity toAdd)
        {
            _dbContext.Set<TEntity>().Add(toAdd);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public virtual void Update(TEntity toUpdate)
        {
            _dbContext.Entry(toUpdate).State = EntityState.Modified;
        }

        public virtual void Update(TEntity toUpdate, UpdateAccessMode accessMode, params string[] propertyNames)
        {
            _dbContext.Attach(toUpdate);
            PropertyInfo[] propertyInfo = typeof(TEntity).GetProperties();
            foreach(var property in propertyInfo)
            {
                //Set defaule not update private key
                var keyName = _dbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties.Select(x => x.Name).Single();
                if (keyName == property.Name)
                {
                    _dbContext.Entry(toUpdate).Property(property.Name).IsModified = false;
                    continue;
                }

                if (accessMode == UpdateAccessMode.ALLOW_UPDATE)
                    _dbContext.Entry(toUpdate).Property(property.Name).IsModified = propertyNames.Contains(property.Name);
                else if(accessMode == UpdateAccessMode.DENY_UPDATE)
                    _dbContext.Entry(toUpdate).Property(property.Name).IsModified = !propertyNames.Contains(property.Name);
            }
        }

        public virtual void Delete(Guid id)
        {
            TEntity entity = FindById(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> condition)
        {
            IQueryable<TEntity> entities = Filter(condition);
            _dbSet.RemoveRange(entities);
        }
        public virtual IQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> condition) => _dbSet.OrderBy(condition);
        public virtual IQueryable<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> condition) => _dbSet.OrderByDescending(condition);

        public virtual bool HasRows(Expression<Func<TEntity, bool>> condition) => _dbSet.Any(condition);

        private bool _disposed = false;

        /// <summary>  
        /// Dispose the object  
        /// </summary>  
        /// <param name="disposing">IsDisposing</param>  
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
                _dbContext?.Dispose();
        }

        /// <summary>  
        /// Dispose the object  
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
