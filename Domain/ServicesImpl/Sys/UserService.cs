using Common.BaseDomain;
using Domain.IRepositorys.Sys;
using Domain.IServices;
using Domain.IServices.Sys;
using Domain.Models.Sys;

namespace Domain.ServicesImpl.Sys
{
    public partial class UserService : BaseService<User, string>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }
    }
}
