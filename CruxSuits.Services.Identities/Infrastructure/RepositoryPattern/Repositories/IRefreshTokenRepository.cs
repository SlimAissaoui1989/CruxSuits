using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Services.Identities.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Identities.Infrastructure.RepositoryPattern.Repositories
{
    public interface IRefreshTokenRepository: IIdentityRepository<long, RefreshToken>
    {
    }
}
