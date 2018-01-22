namespace Common.Base
{
    using Microsoft.AspNetCore.Authorization;
    using System.Collections.Generic;

    public class AuthZRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<PermInfo> PermInfos { get; private set; }

        /// <summary>
        /// 无权限action
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { internal get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="deniedAction">无权限action</param>
        /// <param name="permInfos">用户权限集合</param>
        public AuthZRequirement(string deniedAction, List<PermInfo> permInfos, string claimType)
        {
            ClaimType = claimType;
            DeniedAction = deniedAction;
            PermInfos = permInfos;
        }
    }
}
