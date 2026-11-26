using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Configurations
{
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            DateTime defaultCreatedDate = SharedValues.DefaultCreatedDate;

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
