using Domain.IServices.Sys;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EFCodeDemoWeb.Areas.Sys.Controllers
{
    [Area("Sys")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService usersService)
        {
            _userService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public IActionResult Index()
        {
            var user = new Domain.Models.Sys.User
            {
                Name = "admin",
                TrueName = "管理员",
                CreatTime = DateTime.Now,
                UserInfos = new Domain.Models.Sys.UserInfo { Address = "昆明" }
            };
            _userService.Add(user);//增
            var u = _userService.GetSingle(user.Id);//查
            user.LastLoginTime = DateTime.Now;
            var count1 = _userService.Edit(user);//改
            //var count2 = _userService.Delete(user.Id);//删
            return View();
        }
    }
}