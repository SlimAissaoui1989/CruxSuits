using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Shared.Permissions.UiResources
{
    public class UiResourcePermissionResponse
    {
        public int Id { get; set; }
        public int UiResourceId { get; set; }
        public string Key { get; set; } = null!;
        public bool CanSee { get; set; }
        public bool CanEdit { get; set; }
        public bool IsInheritedFromRole { get; set; }

    }
}
