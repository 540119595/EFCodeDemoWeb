namespace Domain.ServicesImpl.Sys
{
    using System;
	using Domain.IRepositorys.Sys;
	using Domain.IServices;
	using Domain.IServices.Sys;
	using Domain.Models.Sys;
    public partial class RoleService : BaseService<Role, String>, IRoleService
    {
        public RoleService(IRoleRepository repository) : base(repository)
        {
			// 这是ServiceTemplate代码模板生成（添加方法前，请先对接口IRoleService进行修订）
        }
    }
}

