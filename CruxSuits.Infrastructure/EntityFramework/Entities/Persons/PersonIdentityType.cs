using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
{
    public class PersonIdentityType : SoftDeleteEntity<string>
    {
        [Key, MaxLength(10)]
        public override string Id { get; set; } = null!;

        public ICollection<PersonIdentityTypeName>? Names { get; set; } = null!;
    }
}