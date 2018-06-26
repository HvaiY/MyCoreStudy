using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Day24EntityFrameworkCore.controllers;
using Day24EntityFrameworkCore.Models;
using Day24EntityFrameworkCore.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace Day24EntityFrameworkCore.单元测试
{
    public class UserTwoControllerTests
    {
        //使用NUnit 需要NuGet 该包  还需要 NSubstitute包
        private IRepository<UserModel, int> _fakeRepository;
        private UserTwoController _target;

        [SetUp]
        public void SetUp()
        {
            _fakeRepository = Substitute.For<IRepository<UserModel, int>>();
            _target = new UserTwoController(_fakeRepository);
        }
        [Test]
        public void SearchUser()
        {
            // Arrange
            var query = "test";
            var model = new UserModel { Id = 1 };
            _fakeRepository.Find(Arg.Any<Expression<Func<UserModel, bool>>>())
                .Returns(new List<UserModel> { model });

            // Act
            var actual = _target.Get(query);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }
        [Test]
        public void GetUser()
        {
            // Arrange
            var model = new UserModel { Id = 1 };
            _fakeRepository.FindById(Arg.Any<int>()).Returns(model);

            // Act
            var actual = _target.Get(model.Id);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void CreateUser()
        {
            // Arrange
            var model = new UserModel();

            // Act
            var actual = _target.Post(model);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void UpdateUser()
        {
            // Arrange
            var model = new UserModel { Id = 1 };

            // Act
            var actual = _target.Put(model.Id, model);

            // Assert
            Assert.IsTrue(actual.IsSuccess);
        }

        [Test]
        public void DeleteUser()
        {
            // Arrange
            var model = new UserModel { Id = 1 };

            // Act
            var actual = _target.Delete(model.Id);

            // Assert
            //Assert.IsTrue(actual.IsSuccess);
            Assert.Fail();
        }
    }
}
