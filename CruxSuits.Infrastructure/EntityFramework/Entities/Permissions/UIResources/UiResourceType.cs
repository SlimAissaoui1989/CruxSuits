using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class UiResourceType : EntityBase<string>
    {
        [Key, MaxLength(10)]
        public override string Id { get; set; } = null!;

        public bool HasEditPermission { get; set; }

        public bool HasDeletePermission { get; set; }


        public ICollection<UiResourceTypeName>? Names { get; set; }
    }
}
