using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByRoleDetails
{
    public class DataObjectPermissionByRoleDetailRepository : CruxSuitsRepository<int, DataObjectPermissionByRoleDetail>, IDataObjectPermissionByRoleDetailRepository
    {
        public DataObjectPermissionByRoleDetailRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
