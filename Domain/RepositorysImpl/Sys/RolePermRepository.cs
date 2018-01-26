namespace Domain.RepositorysImpl.Sys
{
    using System;
	using Domain.IRepositorys;
	using Domain.IRepositorys.Sys;
	using Domain.Models.Sys;
    public class RolePermRepository : BaseRepository<RolePerm, String>, IRolePermRepository
    {
        public RolePermRepository(DefaultDbContext dbContext) : base(dbContext)
        {
			// 这是RepositoryTemplate.txt代码模板生成（添加方法前，请先对接口IRolePermRepository进行修订）
        }
    }
}
