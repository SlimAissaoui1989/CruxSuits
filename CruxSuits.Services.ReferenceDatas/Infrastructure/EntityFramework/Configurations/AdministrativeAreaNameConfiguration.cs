using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Configurations
{
    internal sealed class AdministrativeAreaNameConfiguration : IEntityTypeConfiguration<AdministrativeAreaName>
    {
        public void Configure(EntityTypeBuilder<AdministrativeAreaName> builder)
        {
            builder.HasIndex(name => new { name.AdministrativeAreaId, name.LanguageId })
                .IsUnique();

            builder.HasOne(name => name.AdministrativeArea)
                .WithMany(area => area.Names)
                .HasForeignKey(name => name.AdministrativeAreaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(name => name.Language)
                .WithMany()
                .HasForeignKey(name => name.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new AdministrativeAreaName { Id = 1, AdministrativeAreaId = "TN-TU", LanguageId = "en", Name = "Tunis Governorate" },
                new AdministrativeAreaName { Id = 2, AdministrativeAreaId = "TN-TU", LanguageId = "ar", Name = "ولاية تونس" },
                new AdministrativeAreaName { Id = 3, AdministrativeAreaId = "TN-TU", LanguageId = "fr", Name = "Gouvernorat de Tunis" },
                new AdministrativeAreaName { Id = 4, AdministrativeAreaId = "TN-AR", LanguageId = "en", Name = "Ariana Governorate" },
                new AdministrativeAreaName { Id = 5, AdministrativeAreaId = "TN-AR", LanguageId = "ar", Name = "ولاية أريانة" },
                new AdministrativeAreaName { Id = 6, AdministrativeAreaId = "TN-AR", LanguageId = "fr", Name = "Gouvernorat de l'Ariana" },
                new AdministrativeAreaName { Id = 7, AdministrativeAreaId = "TN-BA", LanguageId = "en", Name = "Ben Arous Governorate" },
                new AdministrativeAreaName { Id = 8, AdministrativeAreaId = "TN-BA", LanguageId = "ar", Name = "ولاية بن عروس" },
                new AdministrativeAreaName { Id = 9, AdministrativeAreaId = "TN-BA", LanguageId = "fr", Name = "Gouvernorat de Ben Arous" },
                new AdministrativeAreaName { Id = 10, AdministrativeAreaId = "TN-MN", LanguageId = "en", Name = "Manouba Governorate" },
                new AdministrativeAreaName { Id = 11, AdministrativeAreaId = "TN-MN", LanguageId = "ar", Name = "ولاية منوبة" },
                new AdministrativeAreaName { Id = 12, AdministrativeAreaId = "TN-MN", LanguageId = "fr", Name = "Gouvernorat de la Manouba" },
                new AdministrativeAreaName { Id = 13, AdministrativeAreaId = "TN-NB", LanguageId = "en", Name = "Nabeul Governorate" },
                new AdministrativeAreaName { Id = 14, AdministrativeAreaId = "TN-NB", LanguageId = "ar", Name = "ولاية نابل" },
                new AdministrativeAreaName { Id = 15, AdministrativeAreaId = "TN-NB", LanguageId = "fr", Name = "Gouvernorat de Nabeul" },
                new AdministrativeAreaName { Id = 16, AdministrativeAreaId = "TN-ZG", LanguageId = "en", Name = "Zaghouan Governorate" },
                new AdministrativeAreaName { Id = 17, AdministrativeAreaId = "TN-ZG", LanguageId = "ar", Name = "ولاية زغوان" },
                new AdministrativeAreaName { Id = 18, AdministrativeAreaId = "TN-ZG", LanguageId = "fr", Name = "Gouvernorat de Zaghouan" },
                new AdministrativeAreaName { Id = 19, AdministrativeAreaId = "TN-BZ", LanguageId = "en", Name = "Bizerte Governorate" },
                new AdministrativeAreaName { Id = 20, AdministrativeAreaId = "TN-BZ", LanguageId = "ar", Name = "ولاية بنزرت" },
                new AdministrativeAreaName { Id = 21, AdministrativeAreaId = "TN-BZ", LanguageId = "fr", Name = "Gouvernorat de Bizerte" },
                new AdministrativeAreaName { Id = 22, AdministrativeAreaId = "TN-BJ", LanguageId = "en", Name = "Beja Governorate" },
                new AdministrativeAreaName { Id = 23, AdministrativeAreaId = "TN-BJ", LanguageId = "ar", Name = "ولاية باجة" },
                new AdministrativeAreaName { Id = 24, AdministrativeAreaId = "TN-BJ", LanguageId = "fr", Name = "Gouvernorat de Béja" },
                new AdministrativeAreaName { Id = 25, AdministrativeAreaId = "TN-JE", LanguageId = "en", Name = "Jendouba Governorate" },
                new AdministrativeAreaName { Id = 26, AdministrativeAreaId = "TN-JE", LanguageId = "ar", Name = "ولاية جندوبة" },
                new AdministrativeAreaName { Id = 27, AdministrativeAreaId = "TN-JE", LanguageId = "fr", Name = "Gouvernorat de Jendouba" },
                new AdministrativeAreaName { Id = 28, AdministrativeAreaId = "TN-KF", LanguageId = "en", Name = "Kef Governorate" },
                new AdministrativeAreaName { Id = 29, AdministrativeAreaId = "TN-KF", LanguageId = "ar", Name = "ولاية الكاف" },
                new AdministrativeAreaName { Id = 30, AdministrativeAreaId = "TN-KF", LanguageId = "fr", Name = "Gouvernorat du Kef" },
                new AdministrativeAreaName { Id = 31, AdministrativeAreaId = "TN-SL", LanguageId = "en", Name = "Siliana Governorate" },
                new AdministrativeAreaName { Id = 32, AdministrativeAreaId = "TN-SL", LanguageId = "ar", Name = "ولاية سليانة" },
                new AdministrativeAreaName { Id = 33, AdministrativeAreaId = "TN-SL", LanguageId = "fr", Name = "Gouvernorat de Siliana" },
                new AdministrativeAreaName { Id = 34, AdministrativeAreaId = "TN-KR", LanguageId = "en", Name = "Kairouan Governorate" },
                new AdministrativeAreaName { Id = 35, AdministrativeAreaId = "TN-KR", LanguageId = "ar", Name = "ولاية القيروان" },
                new AdministrativeAreaName { Id = 36, AdministrativeAreaId = "TN-KR", LanguageId = "fr", Name = "Gouvernorat de Kairouan" },
                new AdministrativeAreaName { Id = 37, AdministrativeAreaId = "TN-KS", LanguageId = "en", Name = "Kasserine Governorate" },
                new AdministrativeAreaName { Id = 38, AdministrativeAreaId = "TN-KS", LanguageId = "ar", Name = "ولاية القصرين" },
                new AdministrativeAreaName { Id = 39, AdministrativeAreaId = "TN-KS", LanguageId = "fr", Name = "Gouvernorat de Kasserine" },
                new AdministrativeAreaName { Id = 40, AdministrativeAreaId = "TN-SB", LanguageId = "en", Name = "Sidi Bouzid Governorate" },
                new AdministrativeAreaName { Id = 41, AdministrativeAreaId = "TN-SB", LanguageId = "ar", Name = "ولاية سيدي بوزيد" },
                new AdministrativeAreaName { Id = 42, AdministrativeAreaId = "TN-SB", LanguageId = "fr", Name = "Gouvernorat de Sidi Bouzid" },
                new AdministrativeAreaName { Id = 43, AdministrativeAreaId = "TN-SS", LanguageId = "en", Name = "Sousse Governorate" },
                new AdministrativeAreaName { Id = 44, AdministrativeAreaId = "TN-SS", LanguageId = "ar", Name = "ولاية سوسة" },
                new AdministrativeAreaName { Id = 45, AdministrativeAreaId = "TN-SS", LanguageId = "fr", Name = "Gouvernorat de Sousse" },
                new AdministrativeAreaName { Id = 46, AdministrativeAreaId = "TN-MS", LanguageId = "en", Name = "Monastir Governorate" },
                new AdministrativeAreaName { Id = 47, AdministrativeAreaId = "TN-MS", LanguageId = "ar", Name = "ولاية المنستير" },
                new AdministrativeAreaName { Id = 48, AdministrativeAreaId = "TN-MS", LanguageId = "fr", Name = "Gouvernorat de Monastir" },
                new AdministrativeAreaName { Id = 49, AdministrativeAreaId = "TN-MH", LanguageId = "en", Name = "Mahdia Governorate" },
                new AdministrativeAreaName { Id = 50, AdministrativeAreaId = "TN-MH", LanguageId = "ar", Name = "ولاية المهدية" },
                new AdministrativeAreaName { Id = 51, AdministrativeAreaId = "TN-MH", LanguageId = "fr", Name = "Gouvernorat de Mahdia" },
                new AdministrativeAreaName { Id = 52, AdministrativeAreaId = "TN-SF", LanguageId = "en", Name = "Sfax Governorate" },
                new AdministrativeAreaName { Id = 53, AdministrativeAreaId = "TN-SF", LanguageId = "ar", Name = "ولاية صفاقس" },
                new AdministrativeAreaName { Id = 54, AdministrativeAreaId = "TN-SF", LanguageId = "fr", Name = "Gouvernorat de Sfax" },
                new AdministrativeAreaName { Id = 55, AdministrativeAreaId = "TN-GF", LanguageId = "en", Name = "Gafsa Governorate" },
                new AdministrativeAreaName { Id = 56, AdministrativeAreaId = "TN-GF", LanguageId = "ar", Name = "ولاية قفصة" },
                new AdministrativeAreaName { Id = 57, AdministrativeAreaId = "TN-GF", LanguageId = "fr", Name = "Gouvernorat de Gafsa" },
                new AdministrativeAreaName { Id = 58, AdministrativeAreaId = "TN-TZ", LanguageId = "en", Name = "Tozeur Governorate" },
                new AdministrativeAreaName { Id = 59, AdministrativeAreaId = "TN-TZ", LanguageId = "ar", Name = "ولاية توزر" },
                new AdministrativeAreaName { Id = 60, AdministrativeAreaId = "TN-TZ", LanguageId = "fr", Name = "Gouvernorat de Tozeur" },
                new AdministrativeAreaName { Id = 61, AdministrativeAreaId = "TN-KB", LanguageId = "en", Name = "Kebili Governorate" },
                new AdministrativeAreaName { Id = 62, AdministrativeAreaId = "TN-KB", LanguageId = "ar", Name = "ولاية قبلي" },
                new AdministrativeAreaName { Id = 63, AdministrativeAreaId = "TN-KB", LanguageId = "fr", Name = "Gouvernorat de Kébili" },
                new AdministrativeAreaName { Id = 64, AdministrativeAreaId = "TN-GA", LanguageId = "en", Name = "Gabes Governorate" },
                new AdministrativeAreaName { Id = 65, AdministrativeAreaId = "TN-GA", LanguageId = "ar", Name = "ولاية قابس" },
                new AdministrativeAreaName { Id = 66, AdministrativeAreaId = "TN-GA", LanguageId = "fr", Name = "Gouvernorat de Gabès" },
                new AdministrativeAreaName { Id = 67, AdministrativeAreaId = "TN-MD", LanguageId = "en", Name = "Medenine Governorate" },
                new AdministrativeAreaName { Id = 68, AdministrativeAreaId = "TN-MD", LanguageId = "ar", Name = "ولاية مدنين" },
                new AdministrativeAreaName { Id = 69, AdministrativeAreaId = "TN-MD", LanguageId = "fr", Name = "Gouvernorat de Médenine" },
                new AdministrativeAreaName { Id = 70, AdministrativeAreaId = "TN-TT", LanguageId = "en", Name = "Tataouine Governorate" },
                new AdministrativeAreaName { Id = 71, AdministrativeAreaId = "TN-TT", LanguageId = "ar", Name = "ولاية تطاوين" },
                new AdministrativeAreaName { Id = 72, AdministrativeAreaId = "TN-TT", LanguageId = "fr", Name = "Gouvernorat de Tataouine" });
        }
    }
}
