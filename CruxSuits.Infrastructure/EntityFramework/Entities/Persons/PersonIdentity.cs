using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
{
    public class PersonIdentity : EntityIdentityIntKey
    {
        [MaxLength(100)]
        public string FullNumber { get; set; } = null!;

        [MaxLength(80)]
        public string Number { get; set; } = null!;

        public int? PersonId { get; set; }

        public string PersonIdentityTypeId { get; set; } = null!;

        public DateOnly IssuedDate { get; set; }

        public DateOnly? ExpiredDate { get; set; }

        public bool IsExpired { get; set; }


        public Person? Person { get; set; }

        [ForeignKey(nameof(PersonIdentityTypeId))]
        public PersonIdentityType? PersonIdentityType { get; set; }

        public ICollection<DigitalFile>? Files { get; set; }
    }
}