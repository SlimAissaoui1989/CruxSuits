using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
{
    public class Gender : EntityBase<string>
    {
        [Key, MaxLength(1)]
        public override string Id { get; set; } = null!;

        
        public ICollection<GenderName>? Names { get; set; }
    }
}
