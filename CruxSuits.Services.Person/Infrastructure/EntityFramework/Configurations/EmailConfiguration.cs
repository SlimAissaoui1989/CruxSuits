using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class EmailConfiguration : IEntityTypeConfiguration<EMail>
    {
        public void Configure(EntityTypeBuilder<EMail> builder)
        {
            builder.HasIndex(i => i.EMailAddress).IsUnique();

            builder.HasData(new EMail
            {
                Id = 1,
                EMailAddress = Shared.SharedValues.SuperAdminEmail,
                IsValid = true,
                PersonBaseId = Shared.SharedValues.SuperAdminUserId,
                CreatedDate = Shared.SharedValues.DefaultCreatedDate,
            });
        }
    }
}
