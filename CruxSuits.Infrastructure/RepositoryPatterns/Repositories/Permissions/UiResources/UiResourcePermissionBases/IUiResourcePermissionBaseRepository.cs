using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourcePermissionBases
{
    public interface IUiResourcePermissionBaseRepository<TUiResourcePermission> : ICruxSuitsRepository<int, TUiResourcePermission>
        where TUiResourcePermission : UiResourcePermissionBase
    {
    }
}
