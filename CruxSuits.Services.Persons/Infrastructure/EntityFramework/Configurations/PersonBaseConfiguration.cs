using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared.EFConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Persons.Infrastructure.EntityFramework.Configurations
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
