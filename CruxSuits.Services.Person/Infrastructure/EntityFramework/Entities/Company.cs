using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class Company : PersonBase
    {
        public string? OtherName { get; set; }

        public ICollection<CompanyContact>? Contacts { get; set; }

        public ICollection<CompanyLegalIdentity>? LegalIdentities { get; set; }
    }
}
