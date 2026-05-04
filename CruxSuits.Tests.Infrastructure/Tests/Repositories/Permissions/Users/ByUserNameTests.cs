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
    public class ByUserNameTests : BaseTests
    {
        public ByUserNameTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
        }

        //Test UserName NotExist
        [Fact]
        public async Task GetByUserName_ShouldReturnNull_When_UserName_NotExist()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                    await UserData.InitializeTestUserAsync(uow);

                    var user = await uow.User.GetByUserNameAsync("NotExist");
                    user.Should().BeNull();
            }
        }

        //Test UserName 0
        [Fact]
        public async Task GetByUserName_ShouldReturnNull_When_IsDisabled()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                    await UserData.InitializeTestUserAsync(uow, isEnabled: false);

                    var user = await uow.User.GetByUserNameAsync("test.User");
                    user.Should().BeNull();
            }
        }

        //Test UserName 1
        [Fact]
        public async Task GetByUserName_ShouldReturnUser_When_IsEnabled()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                    await UserData.InitializeTestUserAsync(uow);

                    var user = await uow.User.GetByUserNameAsync("test.User");
                    user.Should().NotBeNull();
                    user.UserName.Should().Be("test.User");
            }
        }
    }
}
