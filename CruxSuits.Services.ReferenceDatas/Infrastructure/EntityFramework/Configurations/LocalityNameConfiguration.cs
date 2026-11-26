using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Configurations
{
    internal sealed class LocalityNameConfiguration : IEntityTypeConfiguration<LocalityName>
    {
        public void Configure(EntityTypeBuilder<LocalityName> builder)
        {
            builder.HasIndex(name => new { name.LocalityId, name.LanguageId })
                .IsUnique();

            builder.HasOne(name => name.Locality)
                .WithMany(locality => locality.Names)
                .HasForeignKey(name => name.LocalityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(name => name.Language)
                .WithMany()
                .HasForeignKey(name => name.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(LocalityConfiguration.TunisianMunicipalities.SelectMany((municipality, index) =>
            {
                var localityId = $"{municipality.AdministrativeAreaId}-{municipality.Code}";
                var nameId = index * 3;
                return new[]
                {
                    new LocalityName { Id = nameId + 1, LocalityId = localityId, LanguageId = "en", Name = municipality.LatinName },
                    new LocalityName { Id = nameId + 2, LocalityId = localityId, LanguageId = "ar", Name = municipality.ArabicName },
                    new LocalityName { Id = nameId + 3, LocalityId = localityId, LanguageId = "fr", Name = municipality.LatinName }
                };
            }));
        }
    }
}
