using Domain.Models.Sys;

namespace Domain.IServices.Sys
{
    public partial interface IUserService : IService<User, string>
    {
        string CreateSalt();

        string CreatePasswordHash(string pwd, string strSalt);
    }
}
