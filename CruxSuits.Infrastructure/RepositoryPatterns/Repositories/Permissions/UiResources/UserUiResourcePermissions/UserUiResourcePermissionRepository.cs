using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UserUiResourcePermissions
{
    public class UserUiResourcePermissionRepository : CruxSuitsRepository<int, UserUiResourcePermission>, IUserUiResourcePermissionRepository
    {
        public UserUiResourcePermissionRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
