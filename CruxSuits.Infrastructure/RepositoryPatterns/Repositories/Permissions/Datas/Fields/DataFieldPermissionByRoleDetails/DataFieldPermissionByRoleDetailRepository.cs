using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByRoleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByRoleDetails
{
    public class DataFieldPermissionByRoleDetailRepository : CruxSuitsRepository<int, DataFieldPermissionByRoleDetail>, IDataFieldPermissionByRoleDetailRepository
    {
        public DataFieldPermissionByRoleDetailRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
