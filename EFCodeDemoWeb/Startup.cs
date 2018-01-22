using Common.Base;
using Common.Filters;
using Common.Helpers;
using Domain;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EFCodeDemoWeb
{
    public class Startup
    {
        public IConfiguration _configuration { get; }    // 读配置文件appsettings.json
        public static ILoggerRepository _repository { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;              // 读配置文件appsettings.json

            // 初始化log4net
            _repository = LogManager.CreateRepository("LogRepository");
            Log4NetHelper.SetConfig(_repository, "log4net.config");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 数据库连接
            services.AddDbContext<DefaultDbContext>(o => o.UseMySql(_configuration.GetConnectionString("MySqlConnection"))); // 读配置文件appsettings.json

            // Ioc
            services.AddTransient<Domain.IRepositorys.Sys.IUserRepository, Domain.RepositorysImpl.Sys.UserRepository>();
            services.AddTransient<Domain.IServices.Sys.IUserService, Domain.ServicesImpl.Sys.UserService>();

            #region 授权
            services.AddAuthorization(options =>
            {
                var sp = services.BuildServiceProvider();
                var userService = sp.GetService<Domain.IServices.Sys.IUserService>();
                var user = userService.GetSingle(u => u.Name == "admin1");

                //这个集合模拟用户权限表,可从数据库中查询出来
                var permInfos = new List<PermInfo> {
                    new PermInfo { Name="admin", Url="/Install/Index", Method = "GET"},
                    new PermInfo { Name="admin", Url="/Install", Method = "POST"}
                 };
                //var userService = new UsersService();
                //var uList = userService.GetList(u => u.Id != null);

                //foreach (var u in uList)
                //{
                //    var piList = userService.GePermItemstList(u);
                //    foreach (var pi in piList)
                //    {
                //        permInfos.Add(new PermInfo { Name = u.Name, Url = pi.RouteUrl, Method = pi.HttpMethod });
                //    }
                //}

                //如果第三个参数，是ClaimTypes.Role，上面集合的每个元素的Name为角色名称，如果ClaimTypes.Name，即上面集合的每个元素的Name为用户名
                var authZRequirement = new AuthZRequirement("/Account/Denied", permInfos, ClaimTypes.Name);
                options.AddPolicy("AuthZ",
                          policy => policy.Requirements.Add(authZRequirement));
            })
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = ".AuthZCookie";
                //options.Cookie.Domain = "ynsnjt.com";
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = new PathString("/Account/Denied");
                options.LogoutPath = new PathString("/Account/Logout");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);//.FromSeconds(20);
            });
            #endregion 授权  

            // 服务注入mvc
            //services.AddMvc();
            // 全局异常处理过滤器
            services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
                option.Filters.Add(new GlobalActionFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // 开发异常展示页
            app.UseDeveloperExceptionPage();
            // 浏览器打开链接
            app.UseBrowserLink();

            // 默认文件夹
            app.UseStaticFiles();

            // 路由
            app.UseMvc(routes =>
            {
                // 区域路由
                routes.MapAreaRoute(
                    name: "SysArea",
                    areaName: "Sys",
                    template: "Sys/{controller=User}/{action=Index}/{id?}");

                // 默认路由
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
