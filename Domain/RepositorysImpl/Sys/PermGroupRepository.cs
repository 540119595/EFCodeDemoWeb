namespace Domain.RepositorysImpl.Sys
{
    using System;
	using Common.BaseDomain;
	using Domain.IRepositorys;
	using Domain.IRepositorys.Sys;
	using Domain.Models.Sys;
    public class PermGroupRepository : BaseRepository<PermGroup, String>, IPermGroupRepository
    {
        public PermGroupRepository(DefaultDbContext dbContext) : base(dbContext)
        {
			// 这是RepositoryTemplate.txt代码模板生成（添加方法前，请先对接口IPermGroupRepository进行修订）
        }
    }
}
