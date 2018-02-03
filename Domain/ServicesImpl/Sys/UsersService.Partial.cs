using Common.BaseDomain;
using Domain.IServices;
using Domain.IServices.Sys;
using Domain.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Domain.ServicesImpl.Sys
{
    public partial class UserService : BaseService<User, string>, IUserService
    {
        private const int SALT_LENGTH = 6;  //定义Salt值的长度

        /// <summary>
        /// 创建一个随机的Salt值
        /// </summary>
        /// <returns>随机数的字符串</returns>
        public string CreateSalt()
        {
            //生成一个加密的随机数
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[SALT_LENGTH];
            rng.GetBytes(buff);
            //返回一个Base64随机数的字符串
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 对Salt后的密码进行哈希
        /// </summary>
        /// <param name="pwd">密码</param>
        /// <param name="strSalt">Salt值</param>
        /// <returns>返回加密好的密码</returns>
        public string CreatePasswordHash(string pwd, string strSalt)
        {
            //把密码和Salt连起来
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(pwd + strSalt);
            //对密码进行哈希
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);

            //返回哈希后的值
            return hashString;
        }
        
        public IList<User> GetByCache(Expression<Func<User, bool>> @where = null)
        {
            return Get(where);
        }

        public IList<User> GetByCache2(Expression<Func<User, bool>> @where = null)
        {
            return Get(where);
        }
    }
}
