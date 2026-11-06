using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CruxSuits.Services.Shared.EFConversion;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneType>
    {
        public void Configure(EntityTypeBuilder<PhoneType> builder)
        {
            builder.Property(x => x.Names)
                .HasJsonLongCollectionConversion();

            DateTime now = Shared.SharedValues.DefaultCreatedDate;

            builder.HasData(
                new PhoneType { Id = "Mob", Names = new long[] { 1309, 1310, 1311 }, CreatedDate = now, IsDeleted = false },
                new PhoneType { Id = "Hom", Names = new long[] { 1312, 1313, 1314 }, CreatedDate = now, IsDeleted = false },
                new PhoneType { Id = "Wrk", Names = new long[] { 1315, 1316, 1317 }, CreatedDate = now, IsDeleted = false },
                new PhoneType { Id = "Fax", Names = new long[] { 1318, 1319, 1320 }, CreatedDate = now, IsDeleted = false }
                );
        }
    }
}
