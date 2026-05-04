using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourcePermissionBases
{
    public class UiResourcePermissionBaseRepository<TUiResourcePermission> : CruxSuitsRepository<int, TUiResourcePermission>
        , IUiResourcePermissionBaseRepository<TUiResourcePermission>
        where TUiResourcePermission : UiResourcePermissionBase
    {
        public UiResourcePermissionBaseRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
