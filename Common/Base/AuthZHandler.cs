namespace Common.Base
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;

    public class AuthZHandler : AuthorizationHandler<AuthZRequirement>
    {
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<PermInfo> PermInfos { get; set; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthZRequirement requirement)
        {
            //赋值用户权限
            PermInfos = requirement.PermInfos;
            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();
            var httpMethod = httpContext.Request.Method;
            //是否经过验证
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                //权限中是否存在请求的url
                if (PermInfos.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0
                    && PermInfos.GroupBy(g => g.Method).Where(w => w.Key == httpMethod).Count() > 0)
                {
                    var name = httpContext.User.Claims.SingleOrDefault(s => s.Type == requirement.ClaimType).Value;
                    //验证权限
                    if (PermInfos.Where(w => w.Name == name && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        context.Succeed(requirement);
                    }
                    //else
                    //{
                    //    //无权限跳转到拒绝页面
                    //    httpContext.Response.Redirect(requirement.DeniedAction);
                    //    // TODO:方法2直接提示
                    //}
                }
                //else
                //{
                //    context.Succeed(requirement);
                //}
                else
                {
                    //无权限跳转到拒绝页面
                    httpContext.Response.Redirect(requirement.DeniedAction);
                    // TODO:方法2直接提示
                }
            }
            return Task.CompletedTask;
        }
    }
}
