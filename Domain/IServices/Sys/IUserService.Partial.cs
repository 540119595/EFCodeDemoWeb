using Common.BaseDomain;
using Domain.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.IServices.Sys
{
    public partial interface IUserService : IService<User, string>
    {
        string CreateSalt();

        string CreatePasswordHash(string pwd, string strSalt);

        [Common.Attributes.MemoryCache]
        IList<User> GetByCache(Expression<Func<User, bool>> @where = null);

        [Common.Attributes.RedisCache]
        IList<User> GetByCache2(Expression<Func<User, bool>> @where = null);
    }
}
