using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Configurations
{
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            DateTime defaultCreatedDate = new(2000, 1, 1);

            builder.HasData(
                new Language
                {
                    Id = "ar",
                    EnglishName = "Arabic",
                    NativeName = "العربية",
                    IsRightToLeft = true,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Language
                {
                    Id = "fr",
                    EnglishName = "French",
                    NativeName = "Français",
                    IsRightToLeft = false,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                },
                new Language
                {
                    Id = "en",
                    EnglishName = "English",
                    NativeName = "English",
                    IsRightToLeft = false,
                    CreatedDate = defaultCreatedDate,
                    IsDeleted = false,
                }
            );
        }
    }
}
