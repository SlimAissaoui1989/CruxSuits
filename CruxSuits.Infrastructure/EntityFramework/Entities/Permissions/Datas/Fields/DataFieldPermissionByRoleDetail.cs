using CruxIT.Library.DataToolkit.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields
{
    public class DataFieldPermissionByRoleDetail : DataPermissionLevels
    {
        public int DataFieldPermissionByRoleId { get; set; }

        public string RecordId { get; set; } = null!;

        public DataFieldPermissionByRole? DataFieldPermissionByRole { get; set; }
    }
}