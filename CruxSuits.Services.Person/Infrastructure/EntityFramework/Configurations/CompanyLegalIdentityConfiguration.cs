using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.EFConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class CompanyLegalIdentityConfiguration : IEntityTypeConfiguration<CompanyLegalIdentity>
    {
        public void Configure(EntityTypeBuilder<CompanyLegalIdentity> builder)
        {
            builder.HasIndex(x => x.FullNumber).IsUnique();
            builder.Property(x => x.Files)
                .HasJsonGuidCollectionConversion();
        }
    }
}
