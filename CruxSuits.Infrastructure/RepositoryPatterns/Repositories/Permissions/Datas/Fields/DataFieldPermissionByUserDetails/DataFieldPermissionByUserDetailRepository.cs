using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByUserDetails
{
    public class DataFieldPermissionByUserDetailRepository : CruxSuitsRepository<int, DataFieldPermissionByUserDetail>, IDataFieldPermissionByUserDetailRepository
    {
        public DataFieldPermissionByUserDetailRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
