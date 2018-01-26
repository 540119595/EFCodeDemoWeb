namespace Domain.ServicesImpl.Sys
{
    using System;
	using Domain.IRepositorys.Sys;
	using Domain.IServices;
	using Domain.IServices.Sys;
	using Domain.Models.Sys;
    public partial class PermGroupService : BaseService<PermGroup, String>, IPermGroupService
    {
        public PermGroupService(IPermGroupRepository repository) : base(repository)
        {
			// 这是ServiceTemplate代码模板生成（添加方法前，请先对接口IPermGroupService进行修订）
        }
    }
}

