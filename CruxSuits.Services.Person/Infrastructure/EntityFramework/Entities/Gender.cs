using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities
{
    public class Gender : EntityBase<string>
    {
        [Key, MaxLength(1)]
        public override string Id { get; set; } = null!;


        public ICollection<long>? Names { get; set; }
    }
}