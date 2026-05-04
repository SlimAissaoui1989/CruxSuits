using CruxIT.Library.DataToolkit.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields
{
    public class DataFieldGeneralPermission : DataGeneralPermission
    {
        [ForeignKey(nameof(Id))]
        public DataField? DataField { get; set; }

        public ICollection<DataFieldPermissionByRole>? RolePermissions { get; set; }

        public ICollection<DataFieldPermissionByUser>? UserPermissions { get; set; }
    }
}
