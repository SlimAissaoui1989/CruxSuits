using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public abstract class PersonBase : SoftDeleteEntity<Guid>
    {

        [StringLength(250)]
        public string FullName { get; set; } = null!;


        public ICollection<Address>? Addresses { get; set; }

        public ICollection<Phone>? Phones { get; set; }

        public ICollection<EMail>? EMails { get; set; }

        public ICollection<IBAN>? IBANs { get; set; }

        public ICollection<Guid>? Files { get; set; }
    }
}
