using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.EntityFramework.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities
{
    public class UploadedFileTrace : Entity<long>
    {
        public Guid DigitalFileId { get; set; }

        [MaxLength(255)]
        public string OriginalFileName { get; set; } = null!;

        [MaxLength(255)]
        public string SystemFileName { get; set; } = null!;

        [MaxLength(100)]
        public string FileType { get; set; } = null!;

        [MaxLength(10)]
        public string Extension { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsPublic { get; set; }

        public bool IsSavedInDatabase { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = null!;

        public DateTime ActionDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public ActionTypes ActionType { get; set; }


        public UploadedFile? UploadedFile { get; set; }
    }
}
