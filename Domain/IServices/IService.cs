namespace Domain.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain.Models;

    public interface IService<T, in TKey> : IDisposable where T : IBaseEntity<TKey>
    {
        T GetSingle(TKey key);

        T GetSingle(Expression<Func<T, bool>> @where = null);

        IList<T> Get(Expression<Func<T, bool>> @where = null);

        int Add(T entity);

        int AddRange(ICollection<T> entities);

        int Count(Expression<Func<T, bool>> @where = null);

        int Delete(TKey key);

        int Delete(Expression<Func<T, bool>> @where);

        int Edit(T entity);

        bool Exist(Expression<Func<T, bool>> @where = null);

        int Update(T model, params string[] updateColumns);
    }
}
