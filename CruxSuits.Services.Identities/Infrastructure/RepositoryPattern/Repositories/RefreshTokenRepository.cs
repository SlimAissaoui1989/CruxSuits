using CruxSuits.Services.Identities.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Identities.Infrastructure.RepositoryPattern.Repositories
{
    public class RefreshTokenRepository : IdentityRepository<long, RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IIdentityUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
