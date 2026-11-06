using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class Person : PersonBase
    {
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [MaxLength(100)]
        public string LastName { get; set; } = null!;

        public DateOnly? BirthDate { get; set; }

        public int? BirthCountryId { get; set; }

        [MaxLength(100)]
        public string? BirthCity { get; set; }

        public string? GenderId { get; set; }

        public int? NationalityId { get; set; }


        public Gender? Gender { get; set; }

        public ICollection<PersonIdentity>? PersonIdentities { get; set; }

        public ICollection<CompanyContact>? CompanyResponsibilities { get; set; }
    }
}
