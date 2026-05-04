using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
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


        [ForeignKey(nameof(Id))]
        public User? User { get; set; }

        public Country? BirthCountry { get; set; }

        public Country? NationalityCountry { get; set; }

        public Gender? Gender { get; set; }

        public ICollection<PersonIdentity>? PersonIdentities { get; set; }
    }
}
