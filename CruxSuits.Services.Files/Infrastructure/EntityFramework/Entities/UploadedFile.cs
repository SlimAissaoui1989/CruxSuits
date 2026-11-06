using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities
{
    public class UploadedFile : Entity<Guid>
    {
        [MaxLength(255)]
        public string OriginalFileName { get; set; } = null!;

        [MaxLength(255)]
        public string SystemFileName { get; set; } = null!;

        [MaxLength(10)]
        public string Extension { get; set; } = null!;

        [MaxLength(100)]
        public string FileType { get; set; } = null!;
        
        public string? Description { get; set; }

        public long SizeInBytes { get; set; }

        [MaxLength(2048)]
        public string? AbsolutePath { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[]? Content { get; set; }

        public bool IsPublic { get; set; }

        public bool IsSavedInDatabase { get; set; }
    }
}
