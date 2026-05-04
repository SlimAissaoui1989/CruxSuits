using CruxIT.Library.DataToolkit.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields
{
    public class DataFieldPermissionByUser : DataPermissionLevels
    {
        public string DataFieldGeneralPermissionId { get; set; } = null!;

        public int UserId { get; set; }


        public DataFieldGeneralPermission? DataFieldGeneralPermission { get; set; }

        public User? User { get; set; }

        public ICollection<DataFieldPermissionByUserDetail>? Details { get; set; }
    }
}
