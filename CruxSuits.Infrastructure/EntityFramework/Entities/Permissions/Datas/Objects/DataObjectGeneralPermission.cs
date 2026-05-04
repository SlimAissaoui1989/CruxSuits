using CruxIT.Library.DataToolkit.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects
{
    public class DataObjectGeneralPermission : DataGeneralPermission
    {

        [ForeignKey(nameof(Id))]
        public DataObject? DataObject { get; set; }

        public ICollection<DataObjectPermissionByRole>? RolePermissions { get; set; }

        public ICollection<DataObjectPermissionByUser>? UserPermissions { get; set; }
    }
}
