using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Contacts
{
    public class EMail : EntityIdentityIntKey
    {
        [MaxLength(150)]
        public string EMailAddress { get; set; } = null!;

        public bool IsValid { get; set; }

        public int PersonBaseId { get; set; }


        public PersonBase? PersonBase { get; set; }
    }
}