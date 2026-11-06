using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.EFConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class PersonBaseConfiguration : IEntityTypeConfiguration<PersonBase>
    {
        public void Configure(EntityTypeBuilder<PersonBase> builder)
        {
            builder.Property(x => x.Files)
                .HasJsonGuidCollectionConversion();
        }
    }
}
