using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
{
    public class PersonIdentityTypeName : EntityBase<string>
    {

        [Key, MaxLength(15)]
        public override string Id { get; set; } = null!;

        public string LanguageId { get; set; } = null!;

        public string PersonIdentityTypeId { get; set; } = null!;

        [MaxLength(100)]
        public string Name { get; set; } = null!;


        [ForeignKey(nameof(LanguageId))]
        public Language? Language { get; set; }

        [ForeignKey(nameof(PersonIdentityTypeId))]
        public PersonIdentityType? PersonIdentityType { get; set; }
    }
}