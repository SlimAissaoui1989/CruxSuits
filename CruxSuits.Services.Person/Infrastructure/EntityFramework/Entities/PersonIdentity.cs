using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class PersonIdentity : EntityIdentityIntKey
    {
        [MaxLength(100)]
        public string FullNumber { get; set; } = null!;

        [MaxLength(80)]
        public string Number { get; set; } = null!;

        public Guid? PersonId { get; set; }

        public string PersonIdentityTypeId { get; set; } = null!;

        public DateOnly IssuedDate { get; set; }

        public DateOnly? ExpiredDate { get; set; }

        public bool IsExpired { get; set; }


        public Person? Person { get; set; }

        [ForeignKey(nameof(PersonIdentityTypeId))]
        public PersonIdentityType? PersonIdentityType { get; set; }

        public ICollection<Guid>? Files { get; set; }
    }
}