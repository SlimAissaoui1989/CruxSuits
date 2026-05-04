using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Persons
{
    internal class GenderNameConfiguration : IEntityTypeConfiguration<GenderName>
    {
        public void Configure(EntityTypeBuilder<GenderName> builder)
        {
            builder.HasIndex(i => new { i.GenderId, i.LanguageId }).IsUnique();
            builder.HasData(
                // Male
                new GenderName { Id = "M_EN", GenderId = "M", LanguageId = "EN", Name = "Male" },
                new GenderName { Id = "M_AR", GenderId = "M", LanguageId = "AR", Name = "ذكر" },
                new GenderName { Id = "M_FR", GenderId = "M", LanguageId = "FR", Name = "Homme" },

                // Female
                new GenderName { Id = "F_EN", GenderId = "F", LanguageId = "EN", Name = "Female" },
                new GenderName { Id = "F_AR", GenderId = "F", LanguageId = "AR", Name = "أنثى" },
                new GenderName { Id = "F_FR", GenderId = "F", LanguageId = "FR", Name = "Femme" }
                );
        }
    }
}
