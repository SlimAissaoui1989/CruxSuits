using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class Phone : EntityIdentityIntKey
    {
        [MaxLength(50)]
        public string FullNumber { get; set; } = null!;

        public string CountryId { get; set; } = null!;

        [MaxLength(40)]
        public string Number { get; set; } = null!;

        public string PhoneTypeId { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        public bool IsValid { get; set; }

        public Guid PersonBaseId { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }


        public PhoneType? PhoneType { get; set; }

        public PersonBase? PersonBase { get; set; }
    }
}