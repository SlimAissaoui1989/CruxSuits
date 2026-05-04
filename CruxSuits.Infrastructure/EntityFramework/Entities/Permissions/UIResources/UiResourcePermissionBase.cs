using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources
{
    public class UiResourcePermissionBase : EntityIdentityIntKeyBase
    {
        public int UiResourceId { get; set; }

        public bool CanSee { get; set; }

        public bool CanEdit { get; set; }

        //public bool CanRemoved { get; set; }


        public UiResource? UiResource { get; set; }
    }
}
