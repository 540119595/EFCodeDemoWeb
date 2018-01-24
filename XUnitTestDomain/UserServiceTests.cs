using Domain;
using Domain.Models.Sys;
using Domain.RepositorysImpl.Sys;
using Domain.ServicesImpl.Sys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestDomain
{
    public class UserServiceTests
    {
        private readonly int UTYPE = 1;
        private readonly string EMAIL = "admin@test.com";

        private DefaultDbContext _context = null;

        private UserService _userService = null;

        private User _user = null;
        private UserInfo _userInfo = null;
        private UserGroup _userGroup = null;
        private List<User> _users = null;

        public UserServiceTests()
        {
            var options = new DbContextOptionsBuilder<DefaultDbContext>()
                .UseInMemoryDatabase(databaseName: "MemoryDB")  // 使用内存库
                .Options;
            _context = new DefaultDbContext(options);

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _userService = new UserService(new UserRepository(_context));

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
        }

        [Fact]
        public void Add_2_User_And_IsEqual_Two()
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
    }
}
