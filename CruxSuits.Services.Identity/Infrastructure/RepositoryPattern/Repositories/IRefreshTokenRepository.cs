using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public interface IRefreshTokenRepository: IIdentityRepository<long, RefreshToken>
    {
    }
}
