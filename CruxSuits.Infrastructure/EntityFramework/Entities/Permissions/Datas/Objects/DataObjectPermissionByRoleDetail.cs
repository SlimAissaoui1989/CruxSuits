using CruxIT.Library.DataToolkit.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects
{
    public class DataObjectPermissionByRoleDetail : DataPermissionLevels
    {
        public int DataObjectPermissionByRoleId { get; set; }

        public string RecordId { get; set; } = null!;


        public DataObjectPermissionByRole? DataObjectPermissionByRole { get; set; }
    }
}
