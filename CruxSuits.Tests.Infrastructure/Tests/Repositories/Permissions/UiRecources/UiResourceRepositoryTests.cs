using CruxIT.Library.Exceptions;
using CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.Users;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.UiResources
{
    public class UiResourceRepositoryTests : BaseTests
    {
        public UiResourceRepositoryTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task GetByKeyOrDefault_ShouldReturnNull_When_Key_NotExist()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UiResourceData.InitializeUiResourceAsync(uow);

                var resource = await uow.UiResource.GetByKeyOrDefaultAsync("NotExist");
                resource.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetByKeyOrDefault_ShouldReturnUiResource_When_Key_Exist()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UiResourceData.InitializeUiResourceAsync(uow);

                var resource = await uow.UiResource.GetByKeyOrDefaultAsync("UiResource");
                resource.Should().NotBeNull();
                resource.Key.Should().Be("UiResource");
            }
        }

        [Fact]
        public async Task GetByKey_ShouldThrowCxException_When_Key_NotExist()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UiResourceData.InitializeUiResourceAsync(uow);

                var resource = await uow.UiResource.GetByKeyOrDefaultAsync("NotExist");
                resource.Should().BeNull();
                var ex = await Assert.ThrowsAsync<CxException>(async () =>
                {
                    await uow.UiResource.GetByKeyAsync("NotExist");
                });
                ex.ExceptionType.Should().Be(CxExceptionTypes.NotFound);
            }
        }

        [Fact]
        public async Task GetByKey_ShouldReturnUiResource_When_Key_Exist()
        {
            using (var uow = await Fixture.CreateUnitOfWorkAsync())
            {
                await UiResourceData.InitializeUiResourceAsync(uow);

                var resource = await uow.UiResource.GetByKeyAsync("UiResource");
                resource.Should().NotBeNull();
                resource.Key.Should().Be("UiResource");
            }
        }
    }
}
