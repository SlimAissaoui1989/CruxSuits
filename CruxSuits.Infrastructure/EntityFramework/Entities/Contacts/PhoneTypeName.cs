using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Contacts
{
    public class PhoneTypeName : EntityBase<string>
    {
        [Key, MaxLength(10)]
        public override string Id { get; set; } = null!;

        public string PhoneTypeId { get; set; } = null!;
        
        public string LanguageId { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;


        public PhoneType? PhoneType { get; set; }

        public Language? Language { get; set; } 
    }
}