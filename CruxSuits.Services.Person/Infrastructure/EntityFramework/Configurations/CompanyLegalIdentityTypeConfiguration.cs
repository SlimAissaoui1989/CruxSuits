using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CruxSuits.Services.Shared.EFConversion;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    internal class CompanyLegalIdentityTypeConfiguration : IEntityTypeConfiguration<CompanyLegalIdentityType>
    {
        public void Configure(EntityTypeBuilder<CompanyLegalIdentityType> builder)
        {
            builder.Property(x => x.Names)
                .HasJsonLongCollectionConversion();

            DateTime defaultDate = Shared.SharedValues.DefaultCreatedDate;

            builder.HasData(
                new CompanyLegalIdentityType { Id = "REG", Names = new long[] { 1276, 1277, 1278 }, CreatedDate = defaultDate, IsDeleted = false },
                new CompanyLegalIdentityType { Id = "TAX", Names = new long[] { 1279, 1280, 1281 }, CreatedDate = defaultDate, IsDeleted = false },
                new CompanyLegalIdentityType { Id = "VAT", Names = new long[] { 1282, 1283, 1284 }, CreatedDate = defaultDate, IsDeleted = false },
                new CompanyLegalIdentityType { Id = "LEI", Names = new long[] { 1285, 1286, 1287 }, CreatedDate = defaultDate, IsDeleted = false },
                new CompanyLegalIdentityType { Id = "LIC", Names = new long[] { 1288, 1289, 1290 }, CreatedDate = defaultDate, IsDeleted = false }
            );
        }
    }
}
