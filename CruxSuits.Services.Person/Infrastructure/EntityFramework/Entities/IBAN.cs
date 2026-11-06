using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class IBAN : EntityIdentityIntKey
    {
        [MaxLength(20)]
        public string Number { get; set; } = null!;

        [MaxLength(50)]
        public string BankName { get; set; } = null!;

        public int CountryId { get; set; }

        public Guid PersonBaseId { get; set; }


        public PersonBase? PersonBase { get; set; }

        public ICollection<Guid>? Files { get; set; }
    }
}