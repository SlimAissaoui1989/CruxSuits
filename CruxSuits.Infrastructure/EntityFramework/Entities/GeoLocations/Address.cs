using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations
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


        public Country? Country { get; set; }

        public PersonBase? PersonBase { get; set; }
    }
}
