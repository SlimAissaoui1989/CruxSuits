using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.PermissionBases
{
    public interface IPermissionBaseRepository<TPermission> : ICruxSuitsRepository<int, TPermission>
        where TPermission : UiResourcePermissionBase
    {
    }
}
