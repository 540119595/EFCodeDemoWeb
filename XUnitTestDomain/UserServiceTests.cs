using Common.BaseDomain;
using Domain;
using Domain.Models.Sys;
using Domain.RepositorysImpl.Sys;
using Domain.ServicesImpl.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace XUnitTestDomain
{
    public class UserServiceTests
    {
        private readonly int UTYPE = 1;
        private readonly string EMAIL = "admin@test.com";

        private DefaultDbContext _context = null;

        private UserService _userService = null;
        private UserRepository _userRepository = null;
        private RoleService _roleService = null;
        private UserRoleService _userRoleService = null;

        private User _user = null;
        private UserInfo _userInfo = null;
        private UserGroup _userGroup = null;
        private List<User> _users = null;
        private Role _role = null;
        private UserRole _userRole = null;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<DefaultDbContext>().UseInMemoryDatabase(databaseName: "MemoryDB")  // 使用内存库
                .Options;
            _context = new DefaultDbContext(options);

            _context.Database.EnsureDeleted();  // 删除数据库
            _context.Database.EnsureCreated();  // 创建数据库

            _userRepository = new UserRepository(_context);
            _userService = new UserService(_userRepository);
            _roleService = new RoleService(new RoleRepository(_context));
            _userRoleService = new UserRoleService(new UserRoleRepository(_context));

            _userInfo = new UserInfo { Address = "昆明" };
            _userGroup = new UserGroup { Name = "游客", PId = "-1", Seq = 1, Description = "测试添加，可以删除。" };
            _user = new User
            {
                Name = "admin",
                TrueName = "管理员",
                CreatTime = DateTime.Now,
                Email = EMAIL,
                Phone = "12345678901",
                UType = 1,
                UserInfos = _userInfo,
                UserGroup = _userGroup
            };
            var tmpUser = new User
            {
                Name = "user",
                TrueName = "用户",
                CreatTime = DateTime.Now,
                Email = EMAIL,
                Phone = "12345678901",
                UType = 1,
                UserInfos = _userInfo,
                UserGroup = _userGroup
            };
            _users = new List<User>
            {
                _user,
                tmpUser
            };
            _role = new Role
            {
                Name = "管理员",
                PId = "-1",
                Description = "测试添加，可以删除。"
            };
            _userRole = new UserRole { User = _user, Role = _role };
        }

        [Fact]
        public void Add_2_User_And_IsEqual_Two_ByRepository()
        {
            // Arrange
            _userRepository.AddRange(_users);

            // Act
            var usersCount = _userRepository.Count(u => u.UserGroup.Id == _userGroup.Id);

            // Assert
            Assert.Equal(2, usersCount);
        }

        [Fact]
        public void Add_2_User_And_IsEqual_Two_ByService()
        {
            // Arrange
            _userService.AddRange(_users);

            // Act
            var usersCount = _userService.Count(u => u.UserGroup.Id == _userGroup.Id);

            // Assert
            Assert.Equal(2, usersCount);
        }

        [Fact]
        public void Add_User_IsExist_And_Email_Equal()
        {
            // Arrange
            _userService.Add(_user);

            // Act
            var getUser = _userService.GetSingle(u => u.Id == _user.Id);

            // Assert
            Assert.True(_userService.Exist(u => u.Id == _user.Id));
            Assert.Equal(EMAIL, getUser.Email);
        }

        [Fact]
        public void Add_User_And_Delete_User_IsNotExist()
        {
            // Arrange
            _userService.Add(_user);
            _userService.Delete(_user.Id);

            // Act
            var getUser = _userService.GetSingle(u => u.Id == _user.Id);

            // Assert
            Assert.Null(getUser);
        }

        [Fact]
        public void Add_User_And_Edit_UType_Equal()
        {
            // Arrange
            _userService.Add(_user);

            // Act
            var getUser = _userService.GetSingle(u => u.Id == _user.Id);
            getUser.UType = 2;
            var editCount = _userService.Edit(getUser);

            // Assert
            Assert.NotEqual(UTYPE, getUser.UType);
            Assert.Equal(1, editCount);
        }

        [Fact]
        public void Add_User_And_Update_UType_Equal()
        {
            // Arrange
            _userService.Add(_user);

            // Act
            var getUser = _userService.GetSingle(u => u.Id == _user.Id);
            getUser.UType = 2;
            var editCount = _userService.Update(getUser, new string[] { "UType" });

            // Assert
            Assert.NotEqual(UTYPE, getUser.UType);
            Assert.Equal(1, editCount);
        }

        [Fact]
        public void Add_Role_And_IsExist()
        {
            // Arrange
            _roleService.Add(_role);

            // Act
            var getRole = _roleService.GetSingle(r => r.Id == _role.Id);

            // Assert
            Assert.NotNull(getRole);
        }

        [Fact]
        public void Add_User_Role_And_IsExist()
        {
            // Arrange
            _userService.Add(_user);
            _roleService.Add(_role);
            _userRoleService.Add(_userRole);

            // Act
            var getUserRole = _userRoleService.GetSingle(ur => ur.Id == _userRole.Id);

            // Assert
            Assert.Equal(_user.Id, getUserRole.UserId);
            Assert.Equal(_role.Id, getUserRole.RoleId);
        }

        [Fact]
        public void Add_Users_And_GetTime()
        {
            // Arrange
            for (int i = 0; i < 16; i++)
            {
                User tmpUser = new User
                {
                    Name = "admin" + i,
                    TrueName = "管理员" + i,
                    CreatTime = DateTime.Now,
                    Email = EMAIL,
                    Phone = "12345678901",
                    UType = 1,
                    UserInfos = _userInfo,
                    UserGroup = _userGroup
                };
                _userService.Add(tmpUser);
            }

            // Act
            Stopwatch watch = new Stopwatch();
            watch.Start();//开始计时
            var userList = _userService.Get();
            watch.Stop();//停止计时
            long one = watch.ElapsedMilliseconds;

            watch.Restart();//
            userList = _userService.GetByCache();
            watch.Stop();//停止计时
            long two = watch.ElapsedMilliseconds;

            watch.Restart();//
            userList = _userService.GetByCache();
            watch.Stop();//停止计时
            long three = watch.ElapsedMilliseconds;

            watch.Restart();//
            userList = _userService.GetByCache2();
            watch.Stop();//停止计时
            long four = watch.ElapsedMilliseconds;

            watch.Restart();//
            userList = _userService.GetByCache2();
            watch.Stop();//停止计时
            long five = watch.ElapsedMilliseconds;

            // Assert
            Assert.True(two < one);
            Assert.True(three < one);
        }
    }
}
