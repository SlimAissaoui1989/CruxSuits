using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects
{
    public class DataObject : EntityBase<string>
    {
        [Key,MaxLength(127)]
        public override string Id { get; set; } = null!;


        [ForeignKey(nameof(Id))]
        public DataObjectGeneralPermission? DataObjectGeneralPermission { get; set; }

        public ICollection<DataField>? Fields { get; set; }
    }
}
