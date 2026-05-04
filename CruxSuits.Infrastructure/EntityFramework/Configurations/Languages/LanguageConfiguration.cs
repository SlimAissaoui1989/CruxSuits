using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Languages
{
    internal class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasQueryFilter(f => !f.IsDeleted);

            DateTime now = new DateTime(2025, 01, 01);

            builder.HasData(
                new Language { Id = "FR", Name = "Français", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },
                new Language { Id = "AR", Name = "العربية", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },
                new Language { Id = "EN", Name = "English", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }
                );
        }
    }
}
