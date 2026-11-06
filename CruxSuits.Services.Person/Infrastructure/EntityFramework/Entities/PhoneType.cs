using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class PhoneType : SoftDeleteEntity<string>
    {
        [Key, MaxLength(3)]
        public override string Id { get; set; } = null!;

        public ICollection<long>? Names { get; set; }
    }
}