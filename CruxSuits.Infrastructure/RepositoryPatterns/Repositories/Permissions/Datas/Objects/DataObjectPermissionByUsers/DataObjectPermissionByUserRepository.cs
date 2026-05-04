using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByUsers
{
    public class DataObjectPermissionByUserRepository : CruxSuitsRepository<int, DataObjectPermissionByUser>, IDataObjectPermissionByUserRepository
    {
        public DataObjectPermissionByUserRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
