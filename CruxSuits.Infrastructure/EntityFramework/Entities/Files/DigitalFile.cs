using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Files
{
    public class DigitalFile : SoftDeleteEntity<Guid>
    {

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Type { get; set; } = null!;

        [MaxLength(10)]
        public string Extension { get; set; } = null!;

        public string? Description { get; set; }

        public byte[] Content { get; set; } = null!;
    }
}