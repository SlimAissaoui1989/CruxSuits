using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.PermissionBases
{
    public class PermissionBaseRepository<TPermission> : CruxSuitsRepository<int, TPermission>, IPermissionBaseRepository<TPermission>
        where TPermission : UiResourcePermissionBase
    {
        public PermissionBaseRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
