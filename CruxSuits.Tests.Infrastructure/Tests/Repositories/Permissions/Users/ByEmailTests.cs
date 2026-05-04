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
    public class ByEmailTests : BaseTests
    {
        public ByEmailTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
        }

        //Test ByEmail NotExist
        [Fact]
        public async Task GetByEmail_ShouldReturnNull_When_Email_NotExist()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow);

                var user = await uow.User.GetByEmailAsync("notexist@example.com");
                user.Should().BeNull();
            }
        }

        //Test ByEmail 0 0
        [Fact]
        public async Task GetByEmail_ShouldReturnNull_When_IsDisable_and_Email_IsInvalid()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: false, emailValid: false);

                var user = await uow.User.GetByEmailAsync("user@example.com");
                user.Should().BeNull();
            }
        }

        //Test ByEmail 0 1
        [Fact]
        public async Task GetByEmail_ShouldReturnNull_When_IsDisable_and_Email_IsValid()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: false, emailValid: true);

                var user = await uow.User.GetByEmailAsync("user@example.com");
                user.Should().BeNull();
            }
        }

        //Test ByEmail 1 0
        [Fact]
        public async Task GetByEmail_ShouldReturnNull_When_IsEnable_and_Email_IsInvalid()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: true, emailValid: false);

                var user = await uow.User.GetByEmailAsync("user@example.com");
                user.Should().BeNull();
            }
        }

        //Test ByEmail 1 1
        [Fact]
        public async Task GetByEmail_ShouldReturnUser_When_IsEnable_and_Email_IsValid()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UserData.InitializeTestUserAsync(uow, isEnabled: true, emailValid: true);

                var user = await uow.User.GetByEmailAsync("user@example.com");
                user.Should().NotBeNull();
                user.UserName.Should().Be("test.User");
            }
        }

    }
}
