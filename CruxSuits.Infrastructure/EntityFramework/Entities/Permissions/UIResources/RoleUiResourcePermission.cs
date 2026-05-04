using CruxIT.Library.DataToolkit.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class RoleUiResourcePermission : UiResourcePermissionBase
    {
        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
