using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities
{
    public class UserLoginIdentifier : EntityIdentityIntKey
    {
        public Guid UserId { get; set; }

        [MaxLength(10)]
        public string Type { get; set; } = null!;

        public string NormalizedValue { get; set; } = string.Empty;


        public User? User { get; set; }
    }
}
