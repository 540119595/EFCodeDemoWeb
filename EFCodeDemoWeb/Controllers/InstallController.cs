using Common.CodeGenerator;
using Domain.IRepositorys.Sys;
using Domain.IServices.Sys;
using Domain.Models.Sys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EFCodeDemoWeb.Controllers
{
    public class InstallController : Controller
    {
        private IUserService _userService;
        private IUserRepository _userRepository;
        private IUserGroupService _userGroupService;

        public InstallController(IUserService usersService, IUserGroupService userGroupService, IUserRepository userRepository)
        {
            _userService = usersService ?? throw new ArgumentNullException(nameof(usersService));
            _userGroupService = userGroupService ?? throw new ArgumentNullException(nameof(userGroupService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpGet]
        public IActionResult Index()
        {
            this.CreateDefaultAdmin();
            return View();
        }

        [HttpPost]
        public IActionResult Creating(string x)
        {
            CodeGenerator.Generate();
            return View();
        }

        private void CreateDefaultAdmin()
        {
            List<UserGroup> userGroups = new List<UserGroup>
            {
                new UserGroup{
                     PId = "-1", Name = "系统管理员", Seq = -1, Description = "默认创建用户组，不建议删除。"
                },
                new UserGroup{
                    PId = "-1", Name = "游客", Seq = 1, Description = "默认创建用户组，不建议删除。"
                }
            };
            _userGroupService.AddRange(userGroups);


            var salt = _userService.CreateSalt();
            User user = new User
            {
                Email = "admin@ynsnjt.com",
                Name = "admin",
                TrueName = "管理员",
                Phone = "12345678901",
                Salt = salt,
                Pwd = _userService.CreatePasswordHash("admin", salt),//默认密码
                CreatTime = DateTime.Now,
                GroupId = userGroups.Find(ug => ug.Name == "系统管理员").Id,
                IsEnable = true,
                IsLocked = false,
                LastLoginTime = DateTime.Now,
                UType = (int)Common.Base.UserTypeEnum.PERSONNEL
            };
            if (!_userService.Exist(u => u.Name == user.Name))//默认管理员用户设定
                _userService.Add(user);
        }
    }
}