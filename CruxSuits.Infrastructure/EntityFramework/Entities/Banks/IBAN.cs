using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Banks
{
    public class IBAN : EntityIdentityIntKey
    {
        [MaxLength(20)]
        public string Number { get; set; } = null!;

        [MaxLength(50)]
        public string BankName { get; set; } = null!;

        public int CountryId { get; set; }

        public int PersonBaseId { get; set; }


        public Country? Country { get; set; }

        public PersonBase? PersonBase { get; set; }

        public ICollection<DigitalFile>? Files { get; set; }
    }
}