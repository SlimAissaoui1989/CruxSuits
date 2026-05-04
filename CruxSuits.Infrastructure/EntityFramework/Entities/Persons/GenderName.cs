using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Persons
{
    public class GenderName : EntityBase<string>
    {
        [Key, MaxLength(5)]
        public override string Id { get; set; } = null!;

        public string GenderId { get; set; } = null!;

        public string LanguageId { get; set; } = null!;

        [MaxLength(50)]
        public string Name { get; set; } = null!;


        public Gender? Gender { get; set; }

        public Language? Language { get; set; }
    }
}