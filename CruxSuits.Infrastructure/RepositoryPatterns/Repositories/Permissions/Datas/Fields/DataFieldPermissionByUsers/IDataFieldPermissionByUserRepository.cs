using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByUsers
{
    public interface IDataFieldPermissionByUserRepository : ICruxSuitsRepository<int, DataFieldPermissionByUser>
    {
    }
}
