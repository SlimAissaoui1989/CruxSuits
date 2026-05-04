using CruxIT.Library.DataToolkit.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects
{
    public class DataObjectPermissionByUserDetail : DataPermissionLevels
    {
        public int DataObjectPermissionByUserId { get; set; }

        public string RecordId { get; set; } = null!;

        public DataObjectPermissionByUser? DataObjectPermissionByUser { get; set; }
    }
}