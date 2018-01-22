namespace Domain.IRepositorys
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;
    using Domain.Models;

    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class, IBaseEntity<TKey>
    {
        private readonly DefaultDbContext _dbContext;
        private readonly DbSet<T> _set;

        public BaseRepository(DefaultDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbContext.Database.EnsureCreated();
            _set = dbContext.Set<T>();
        }

        public virtual T GetSingle(TKey key)
        {
            return _set.Find(key);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> @where = null)
        {
            if (where == null) return _set.SingleOrDefault();
            return _set.SingleOrDefault(@where);
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> @where = null)
        {
            return (@where != null ? _set.AsNoTracking().Where(@where) : _set.AsNoTracking()).ToList();
        }

        public virtual int Add(T entity)
        {
            _set.Add(entity);
            return DoSave();
        }

        public virtual int AddRange(ICollection<T> entities)
        {
            _set.AddRange(entities);
            return DoSave();
        }

        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return where == null ? _set.Count() : _set.Count(@where);
        }

        public virtual int Delete(TKey key)
        {
            var entity = _set.Find(key);
            if (entity == null) return 0;
            _set.Remove(entity);
            return DoSave();
        }

        public virtual int Delete(Expression<Func<T, bool>> @where)
        {
            return _dbContext.Delete(where);
        }

        public virtual int Edit(T entity)
        {
            //_dbContext.Entry<T>(entity).State = EntityState.Modified;
            //return Save();
            //演变而来
            return _dbContext.Edit(entity);
        }

        public virtual bool Exist(Expression<Func<T, bool>> @where = null)
        {
            return Get(where).Any();
        }

        public virtual int Update(T model, params string[] updateColumns)
        {
            return _dbContext.Update(model, updateColumns);
        }

        public virtual int DoSave()
        {
            return _dbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
