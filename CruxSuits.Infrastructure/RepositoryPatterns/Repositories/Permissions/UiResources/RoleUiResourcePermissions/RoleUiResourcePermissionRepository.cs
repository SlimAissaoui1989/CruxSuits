using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourcePermissionBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.RoleUiResourcePermissions
{
    public class RoleUiResourcePermissionRepository : UiResourcePermissionBaseRepository<RoleUiResourcePermission>
        , IRoleUiResourcePermissionRepository
    {
        public RoleUiResourcePermissionRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
