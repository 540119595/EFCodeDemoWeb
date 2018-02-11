namespace Common.BaseDomain
{
    /// <summary>
    /// 所有数据表实体类都必须实现此接口
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
