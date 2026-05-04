using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.RefreshTokens
{
    public class RefreshTokenRepository : CruxSuitsRepository<int, RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
            UseTrace = false;
        }

        public override Task<RefreshToken> AddAsync(RefreshToken entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            return base.AddAsync(entity);
        }
    }
}
