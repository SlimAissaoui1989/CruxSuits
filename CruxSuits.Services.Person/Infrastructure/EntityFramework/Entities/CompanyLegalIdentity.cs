using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class CompanyLegalIdentity : EntityIdentityIntKey
    {
        [MaxLength(100)]
        public string FullNumber { get; set; } = null!;

        [MaxLength(80)]
        public string Number { get; set; } = null!;

        public Guid CompanyId { get; set; }

        public string CompanyLegalIdentityTypeId { get; set; } = null!;

        public DateOnly? IssuedDate { get; set; }

        public DateOnly? ExpiredDate { get; set; }

        public bool? IsExpired { get; set; }


        public Company? Company { get; set; }

        [ForeignKey(nameof(CompanyLegalIdentityTypeId))]
        public CompanyLegalIdentityType? CompanyLegalIdentityType { get; set; }

        public ICollection<Guid>? Files { get; set; }
    }
}
