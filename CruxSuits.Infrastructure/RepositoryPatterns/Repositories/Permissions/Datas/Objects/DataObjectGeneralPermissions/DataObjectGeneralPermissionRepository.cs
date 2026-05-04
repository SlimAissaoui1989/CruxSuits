using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectGeneralPermissions
{
    public class DataObjectGeneralPermissionRepository : CruxSuitsRepository<string, DataObjectGeneralPermission>
        , IDataObjectGeneralPermissionRepository
    {
        public DataObjectGeneralPermissionRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
