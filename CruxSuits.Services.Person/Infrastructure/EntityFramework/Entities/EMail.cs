using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class EMail : EntityIdentityIntKey
    {
        [MaxLength(150)]
        public string EMailAddress { get; set; } = null!;

        public bool IsValid { get; set; }

        public Guid PersonBaseId { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }


        public PersonBase? PersonBase { get; set; }
    }
}