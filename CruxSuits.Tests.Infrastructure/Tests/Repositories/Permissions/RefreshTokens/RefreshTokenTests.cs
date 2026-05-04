using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.Users;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.RefreshTokens
{
    public class RefreshTokenTests : BaseTests
    {
        public RefreshTokenTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
        }


        [Fact]
        public async Task AddRefreshToken_ShouldFillCreateDate()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                DateTime nowUtc = DateTime.UtcNow;

                RefreshToken refreshToken = new()
                {
                    AcessToken = "access_token",
                    DeviceId = Guid.NewGuid(),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    IsRevoked = false,
                    Password = "password",
                    RefreshTokenValue = "refreshToken",
                    UserId = 1,
                    UserName = "slim.aissaoui",
                };
                await uow.RefreshToken.AddAsync(refreshToken);
                await uow.SaveChangesAsync();
                refreshToken.CreatedDate.Should().BeAfter(nowUtc);
            }
        }
    }
}
