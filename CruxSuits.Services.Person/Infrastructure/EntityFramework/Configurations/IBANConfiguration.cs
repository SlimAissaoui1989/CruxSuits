using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.EFConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class IBANConfiguration : IEntityTypeConfiguration<IBAN>
    {
        public void Configure(EntityTypeBuilder<IBAN> builder)
        {
            builder.Property(x => x.Files)
                .HasJsonGuidCollectionConversion();
        }
    }
}
