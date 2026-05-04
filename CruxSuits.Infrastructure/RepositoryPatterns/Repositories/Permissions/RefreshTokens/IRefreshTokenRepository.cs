using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.RefreshTokens
{
    public interface IRefreshTokenRepository : ICruxSuitsRepository<int, RefreshToken>
    {
    }
}
