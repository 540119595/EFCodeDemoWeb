using Common.BaseDomain;
using Domain.Models.Sys;

namespace Domain.IServices.Sys
{
    public partial interface IUserService : IService<User, string>
    {
    }
}
