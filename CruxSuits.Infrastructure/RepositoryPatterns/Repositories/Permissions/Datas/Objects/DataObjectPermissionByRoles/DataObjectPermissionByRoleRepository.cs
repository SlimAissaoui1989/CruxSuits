using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByRoles
{
    public class DataObjectPermissionByRoleRepository : CruxSuitsRepository<int, DataObjectPermissionByRole>, IDataObjectPermissionByRoleRepository
    {
        public DataObjectPermissionByRoleRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
