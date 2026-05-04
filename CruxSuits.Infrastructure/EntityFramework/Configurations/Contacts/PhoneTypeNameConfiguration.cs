using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Contacts
{
    internal class PhoneTypeNameConfiguration : IEntityTypeConfiguration<PhoneTypeName>
    {
        public void Configure(EntityTypeBuilder<PhoneTypeName> builder)
        {
            DateTime now = DateTime.Now;

            builder.HasData(
                // Mobile
                new PhoneTypeName { Id = "Mob_EN", PhoneTypeId = "Mob", LanguageId = "EN", Name = "Mobile" },
                new PhoneTypeName { Id = "Mob_AR", PhoneTypeId = "Mob", LanguageId = "AR", Name = "جوال" },
                new PhoneTypeName { Id = "Mob_FR", PhoneTypeId = "Mob", LanguageId = "FR", Name = "Mobile" },

                // Home
                new PhoneTypeName { Id = "Hom_EN", PhoneTypeId = "Hom", LanguageId = "EN", Name = "Home" },
                new PhoneTypeName { Id = "Hom_AR", PhoneTypeId = "Hom", LanguageId = "AR", Name = "المنزل" },
                new PhoneTypeName { Id = "Hom_FR", PhoneTypeId = "Hom", LanguageId = "FR", Name = "Domicile" },

                // Work
                new PhoneTypeName { Id = "Wrk_EN", PhoneTypeId = "Wrk", LanguageId = "EN", Name = "Work" },
                new PhoneTypeName { Id = "Wrk_AR", PhoneTypeId = "Wrk", LanguageId = "AR", Name = "العمل" },
                new PhoneTypeName { Id = "Wrk_FR", PhoneTypeId = "Wrk", LanguageId = "FR", Name = "Travail" },

                // Fax
                new PhoneTypeName { Id = "Fax_EN", PhoneTypeId = "Fax", LanguageId = "EN", Name = "Fax" },
                new PhoneTypeName { Id = "Fax_AR", PhoneTypeId = "Fax", LanguageId = "AR", Name = "فاكس" },
                new PhoneTypeName { Id = "Fax_FR", PhoneTypeId = "Fax", LanguageId = "FR", Name = "Fax" }
                );
        }
    }
}