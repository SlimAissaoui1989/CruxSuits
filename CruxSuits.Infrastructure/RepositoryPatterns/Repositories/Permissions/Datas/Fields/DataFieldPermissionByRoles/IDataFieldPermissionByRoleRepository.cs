using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByRoles
{
    public interface IDataFieldPermissionByRoleRepository : ICruxSuitsRepository<int, DataFieldPermissionByRole>
    {
    }
}
