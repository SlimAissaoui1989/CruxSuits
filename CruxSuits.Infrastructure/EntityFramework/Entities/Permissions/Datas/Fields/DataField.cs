using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields
{
    public class DataField : EntityBase<string>
    {
        [Key, MaxLength(127)]
        public override string Id { get; set; } = null!;

        [MaxLength(127)]
        public string ShortId { get; set; } = null!;

        public string DataObjectId { get; set; } = null!;


        [ForeignKey(nameof(Id))]
        public DataFieldGeneralPermission? DataFieldGeneralPermission { get; set; }

        public DataObject? DataObject { get; set; }
    }
}