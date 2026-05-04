using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Banks;
using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
{
    public class PersonBase : SoftDeletedEntityIdentityIntKey
    {
        [StringLength(250)]
        public string FullName { get; set; } = null!;


        public ICollection<Address>? Addresses { get; set; }

        public ICollection<Phone>? Phones { get; set; }

        public ICollection<EMail>? EMails { get; set; }

        public ICollection<IBAN>? IBANs { get; set; }

        public ICollection<DigitalFile>? Files { get; set; }
    }
}
