using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.Users
{
    public class ByPhoneTests : BaseTests
    {
        public ByPhoneTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
        }

        //Test ByPhone NotExist
        [Fact]
        public async Task GetByPhone_ShouldReturnNull_When_Phone_NotExist()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow);

                var user = await uow.User.GetByPhoneAsync("0000");
                user.Should().BeNull();
            }
        }

        //Test ByPhone 0 0
        [Fact]
        public async Task GetByPhone_ShouldReturnNull_When_IsDisable_and_Phone_IsInvalid()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: false, phoneValid: false);

                var user = await uow.User.GetByPhoneAsync("123456");
                user.Should().BeNull();
            }
        }

        //Test ByPhone 0 1
        [Fact]
        public async Task GetByPhone_ShouldReturnNull_When_IsDisable_and_Phone_IsValid()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: false, phoneValid: true);

                var user = await uow.User.GetByPhoneAsync("123456");
                user.Should().BeNull();
            }
        }

        //Test ByPhone 1 0
        [Fact]
        public async Task GetByPhone_ShouldReturnNull_When_IsEnable_and_Phone_IsInvalid()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: true, phoneValid: false);

                var user = await uow.User.GetByPhoneAsync("123456");
                user.Should().BeNull();
            }
        }

        //Test ByPhone 1 1
        [Fact]
        public async Task GetByPhone_ShouldReturnUser_When_IsEnable_and_Phone_IsValid()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: true, phoneValid: true);

                var user = await uow.User.GetByPhoneAsync("123456");
                user.Should().NotBeNull();
                user.UserName.Should().Be("test.User");
            }
        }
    }
}
