using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Shared.Permissions.UiResources
{
    public class UiResourceResponse
    {
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string? Name { get; set; }
        public string UiResourceTypeId { get; set; } = null!;
        public string? UiResourceTypeName { get; set; }
    }
}
