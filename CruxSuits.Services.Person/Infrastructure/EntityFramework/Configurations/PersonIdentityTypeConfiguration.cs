using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CruxSuits.Services.Shared.EFConversion;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class PersonIdentityTypeConfiguration : IEntityTypeConfiguration<PersonIdentityType>
    {
        public void Configure(EntityTypeBuilder<PersonIdentityType> builder)
        {
            builder.Property(x => x.Names)
                .HasJsonLongCollectionConversion();

            DateTime defaultDate = Shared.SharedValues.DefaultCreatedDate;

            builder.HasData(
                new PersonIdentityType { Id = "PASS", Names = new long[] { 1291, 1292, 1293 }, CreatedDate = defaultDate, IsDeleted = false },
                new PersonIdentityType { Id = "ID", Names = new long[] { 1294, 1295, 1296 }, CreatedDate = defaultDate, IsDeleted = false },
                new PersonIdentityType { Id = "DL", Names = new long[] { 1297, 1298, 1299 }, CreatedDate = defaultDate, IsDeleted = false },
                new PersonIdentityType { Id = "RC", Names = new long[] { 1300, 1301, 1302 }, CreatedDate = defaultDate, IsDeleted = false }
                );
        }
    }
}
