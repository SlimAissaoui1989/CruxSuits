using CruxIT.Library.DataToolkit.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields
{
    public class DataFieldPermissionByUserDetail : DataPermissionLevels
    {
        public int DataFieldPermissionByUserId { get; set; }

        public string RecordId { get; set; } = null!;


        public DataFieldPermissionByUser? DataFieldPermissionByUser { get; set; }
    }
}