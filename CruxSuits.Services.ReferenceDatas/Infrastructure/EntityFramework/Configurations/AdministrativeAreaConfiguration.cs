using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Configurations
{
    internal sealed class AdministrativeAreaConfiguration : IEntityTypeConfiguration<AdministrativeArea>
    {
        public void Configure(EntityTypeBuilder<AdministrativeArea> builder)
        {
            builder.HasIndex(area => new { area.CountryId, area.Code })
                .IsUnique();

            builder.HasOne(area => area.Country)
                .WithMany()
                .HasForeignKey(area => area.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            var defaultCreatedDate = Shared.SharedValues.DefaultCreatedDate;
            builder.HasData(
                new AdministrativeArea { Id = "TN-TU", CountryId = "TN", Code = "TU", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-AR", CountryId = "TN", Code = "AR", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-BA", CountryId = "TN", Code = "BA", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-MN", CountryId = "TN", Code = "MN", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-NB", CountryId = "TN", Code = "NB", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-ZG", CountryId = "TN", Code = "ZG", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-BZ", CountryId = "TN", Code = "BZ", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-BJ", CountryId = "TN", Code = "BJ", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-JE", CountryId = "TN", Code = "JE", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-KF", CountryId = "TN", Code = "KF", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-SL", CountryId = "TN", Code = "SL", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-KR", CountryId = "TN", Code = "KR", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-KS", CountryId = "TN", Code = "KS", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-SB", CountryId = "TN", Code = "SB", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-SS", CountryId = "TN", Code = "SS", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-MS", CountryId = "TN", Code = "MS", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-MH", CountryId = "TN", Code = "MH", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-SF", CountryId = "TN", Code = "SF", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-GF", CountryId = "TN", Code = "GF", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-TZ", CountryId = "TN", Code = "TZ", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-KB", CountryId = "TN", Code = "KB", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-GA", CountryId = "TN", Code = "GA", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-MD", CountryId = "TN", Code = "MD", CreatedDate = defaultCreatedDate },
                new AdministrativeArea { Id = "TN-TT", CountryId = "TN", Code = "TT", CreatedDate = defaultCreatedDate });
        }
    }
}
