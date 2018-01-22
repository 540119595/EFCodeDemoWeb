namespace Domain.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain.IRepositorys;
    using Domain.Models;

    public abstract class BaseService<T, TKey> : IService<T, TKey> where T : class, IBaseEntity<TKey>
    {
        protected IRepository<T, TKey> _repository;

        protected BaseService(IRepository<T, TKey> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public virtual T GetSingle(TKey key)
        {
            return _repository.GetSingle(key);
        }

        public virtual T GetSingle(Expression<Func<T, bool>> @where = null)
        {
            return _repository.GetSingle(where);
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> @where = null)
        {
            return _repository.Get(where);
        }

        public virtual int Add(T entity)
        {
            return _repository.Add(entity);
        }

        public virtual int AddRange(ICollection<T> entities)
        {
            return _repository.AddRange(entities);
        }

        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return _repository.Count(where);
        }

        public virtual int Delete(TKey key)
        {
            return _repository.Delete(key);
        }

        public virtual int Delete(Expression<Func<T, bool>> @where)
        {
            return _repository.Delete(where);
        }

        public virtual int Edit(T entity)
        {
            return _repository.Edit(entity);
        }

        public bool Exist(Expression<Func<T, bool>> @where = null)
        {
            return _repository.Exist(where);
        }

        public virtual int Update(T model, params string[] updateColumns)
        {
            return _repository.Update(model, updateColumns);
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }
    }
}
