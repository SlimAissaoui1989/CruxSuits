using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities
{
    public class CompanyLegalIdentityType : SoftDeleteEntity<string>
    {
        [Key, MaxLength(10)]
        public override string Id { get; set; } = null!;

        public ICollection<long>? Names { get; set; }
    }
}
