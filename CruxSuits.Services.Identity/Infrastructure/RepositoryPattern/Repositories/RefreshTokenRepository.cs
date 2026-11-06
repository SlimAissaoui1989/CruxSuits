using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public class RefreshTokenRepository : IdentityRepository<long, RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IIdentityUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
