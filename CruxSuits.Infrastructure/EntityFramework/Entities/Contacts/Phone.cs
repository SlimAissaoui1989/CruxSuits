using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Contacts
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

        public int PersonBaseId { get; set; }


        public PhoneType? PhoneType { get; set; }

        public Country? Country { get; set; }

        public PersonBase? PersonBase { get; set; }
    }
}