using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Contacts
{
    public class PhoneType : SoftDeleteEntity<string>
    {
        [Key, MaxLength(3)]
        public override string Id { get; set; } = null!;

        public ICollection<PhoneTypeName>? Names { get; set; }
    }
}
