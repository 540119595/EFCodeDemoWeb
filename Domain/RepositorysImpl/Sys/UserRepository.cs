using Domain.IRepositorys;
using Domain.IRepositorys.Sys;
using Domain.Models.Sys;

namespace Domain.RepositorysImpl.Sys
{
    public class UserRepository : BaseRepository<User, string>, IUserRepository
    {
        public UserRepository(DefaultDbContext dbContext) : base(dbContext)
        {
        }
    }
}
