namespace Common.BaseDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T, TKey> : IDisposable where T : class, IBaseEntity<TKey>
    {
        /// <summary>
        /// 通过主键获取单实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetSingle(TKey key);

        /// <summary>
        /// 通过条件获取单实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 通过条件获取列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IList<T> Get(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int AddRange(ICollection<T> entities);

        /// <summary>
        /// 实体数量
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> @where = null);

        /// <summary>
        /// 通过主键删除实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        int Delete(TKey key);

        /// <summary>
        /// 通过条件删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> @where);

        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Edit(T entity);

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Exist(Expression<Func<T, bool>> @where = null);

        int Update(T model, params string[] updateColumns);

        /// <summary>
        /// SaveChanges动作
        /// </summary>
        /// <returns></returns>
        int DoSave();
    }
}
