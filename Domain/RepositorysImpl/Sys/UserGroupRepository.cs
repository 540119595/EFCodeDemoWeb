namespace Domain.RepositorysImpl.Sys
{
    using System;
	using Common.BaseDomain;
	using Domain.IRepositorys;
	using Domain.IRepositorys.Sys;
	using Domain.Models.Sys;
    public class UserGroupRepository : BaseRepository<UserGroup, String>, IUserGroupRepository
    {
        public UserGroupRepository(DefaultDbContext dbContext) : base(dbContext)
        {
			// 这是RepositoryTemplate.txt代码模板生成（添加方法前，请先对接口IUserGroupRepository进行修订）
        }
    }
}
