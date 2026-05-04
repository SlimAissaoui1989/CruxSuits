using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Roles
{
    public interface IRoleRepository : ICruxSuitsRepository<int, Role>
    {
    }
}
