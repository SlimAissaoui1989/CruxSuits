using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CruxSuits.Services.Shared.EFConversion;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(x => x.Names)
                .HasJsonLongCollectionConversion();

            builder.HasData(
                new Gender { Id = "M", Names = new long[] { 1303, 1304, 1305 } },
                new Gender { Id = "F", Names = new long[] { 1306, 1307, 1308 } }
                );
        }
    }
}
