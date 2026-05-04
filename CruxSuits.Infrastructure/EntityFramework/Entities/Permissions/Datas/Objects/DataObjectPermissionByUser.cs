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
    public class DataObjectPermissionByUser : DataPermissionLevels
    {
        public string DataObjectGeneralPermissionId { get; set; } = null!;

        public int UserId { get; set; }


        public DataObjectGeneralPermission? DataObjectGeneralPermission { get; set; }

        public User? User{ get; set; }

        public ICollection<DataObjectPermissionByUserDetail>? Details { get; set; }
    }
}
