using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Contacts
{
    internal class EMailConfiguration : IEntityTypeConfiguration<EMail>
    {
        public void Configure(EntityTypeBuilder<EMail> builder)
        {
            builder.HasData(new EMail
            {
                Id = 1,
                EMailAddress = "slim.aissaoui89@gmail.com",
                IsValid = true,
                PersonBaseId = 1,
                CreatedDate = new DateTime(2025,01,01),
                CreatedUserId = 0,
                CreatedUserName = "system",
            });
        }
    }
}
