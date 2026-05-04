using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Shared.Permissions.UiResources
{
    public class UiResourceRequest
    {
        [MaxLength(50)]
        public string Key { get; set; } = null!;

        [MaxLength(511)]
        public string? ParentUiResourceKey { get; set; }
         
        [MaxLength(10)]
        public string UiResourceTypeId { get; set; } = null!;

        public Dictionary<string, string> Names { get; set; } = null!;
    }
}
