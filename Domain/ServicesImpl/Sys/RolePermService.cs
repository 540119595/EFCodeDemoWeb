namespace Domain.ServicesImpl.Sys
{
    using System;	
	using Common.BaseDomain;
	using Domain.IRepositorys.Sys;
	using Domain.IServices;
	using Domain.IServices.Sys;
	using Domain.Models.Sys;
    public partial class RolePermService : BaseService<RolePerm, String>, IRolePermService
    {
        public RolePermService(IRolePermRepository repository) : base(repository)
        {
			// 这是ServiceTemplate代码模板生成（添加方法前，请先对接口IRolePermService进行修订）
        }
    }
}

