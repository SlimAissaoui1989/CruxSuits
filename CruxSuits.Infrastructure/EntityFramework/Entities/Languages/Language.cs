using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Languages
{
    public class Language : SoftDeleteEntity<string>
    {
        [Key, MaxLength(5)]
        public override string Id { get; set; } = null!;

        [MaxLength(20)]
        public string Name { get; set; } = null!;
    }
}