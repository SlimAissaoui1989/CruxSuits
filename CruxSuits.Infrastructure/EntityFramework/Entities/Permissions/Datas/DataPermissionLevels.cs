using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas
{
    public class DataPermissionLevels : EntityIdentityIntKeyBase
    {
        public bool CanVisible { get; set; }

        public bool CanEdited { get; set; }

        public bool CanRemoved { get; set; }
    }
}
