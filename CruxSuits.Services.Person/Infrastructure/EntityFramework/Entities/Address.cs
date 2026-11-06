using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class Address : EntityIdentityIntKey
    {
        [MaxLength(500)]
        public string FullAddress { get; set; } = null!;

        [MaxLength(250)]
        public string AddressString { get; set; } = null!;

        [MaxLength(10)]
        public string PostalCode { get; set; } = null!;

        [MaxLength(100)]
        public string City { get; set; } = null!;

        public int CountryId { get; set; }

        public int PersonBaseId { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        public PersonBase? PersonBase { get; set; }
    }
}