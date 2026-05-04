using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class UserUiResourcePermission : UiResourcePermissionBase
    {
        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
