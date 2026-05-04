using CruxIT.Library.DataToolkit.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields
{
    public class DataFieldPermissionByRole : DataPermissionLevels
    {
        public string DataFieldGeneralPermissionId { get; set; } = null!;

        public int RoleId { get; set; }


        public DataFieldGeneralPermission? DataFieldGeneralPermission { get; set; }

        public Role? Role { get; set; }

        public ICollection<DataFieldPermissionByRoleDetail>? Details { get; set; }
    }
}
