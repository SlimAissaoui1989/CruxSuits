using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class CompanyContact: EntityIdentityIntKey
    {
        public Guid PersonId { get; set; }

        public Guid CompanyId { get; set; }

        [MaxLength(100)]
        public string? Role { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        public bool IsResponsible { get; set; }


        public Person? Person { get; set; }

        public Company? Company { get; set; }
    }
}
