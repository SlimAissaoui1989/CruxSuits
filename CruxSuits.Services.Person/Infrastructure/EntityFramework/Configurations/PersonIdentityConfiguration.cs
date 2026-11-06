using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.EFConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class PersonIdentityConfiguration : IEntityTypeConfiguration<PersonIdentity>
    {
        public void Configure(EntityTypeBuilder<PersonIdentity> builder)
        {
            builder.HasIndex(x => x.FullNumber).IsUnique();
            builder.Property(x => x.Files)
                .HasJsonGuidCollectionConversion();
        }
    }
}
