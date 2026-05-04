using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.EntityFramework.Enums;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Entities.Files
{
    public class DigitalFileTrace : EntityIdentityIntKey
    {
        public Guid DigitalFileId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string Type { get; set; } = null!;

        [MaxLength(10)]
        public string Extension { get; set; } = null!;

        public string? Description { get; set; }

        public int UserId { get; set; }

        public DateTime ActionDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public ActionTypes ActionType { get; set; }


        public User? User { get; set; }
    }
}
