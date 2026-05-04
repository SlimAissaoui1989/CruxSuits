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
    internal class PersonIdentityTypeNameConfiguration : IEntityTypeConfiguration<PersonIdentityTypeName>
    {
        public void Configure(EntityTypeBuilder<PersonIdentityTypeName> builder)
        {
            builder.HasKey(k => new { k.LanguageId, k.PersonIdentityTypeId });
            builder.HasData(
                new PersonIdentityTypeName { Id = "PASS_FR", LanguageId = "FR", PersonIdentityTypeId = "PASS", Name = "Passeport" },
                new PersonIdentityTypeName { Id = "PASS_AR", LanguageId = "AR", PersonIdentityTypeId = "PASS", Name = "جواز السفر" },
                new PersonIdentityTypeName { Id = "PASS_EN", LanguageId = "EN", PersonIdentityTypeId = "PASS", Name = "Passport" },

                new PersonIdentityTypeName { Id = "ID_FR", LanguageId = "FR", PersonIdentityTypeId = "ID", Name = "Carte d'identité" },
                new PersonIdentityTypeName { Id = "ID_AR", LanguageId = "AR", PersonIdentityTypeId = "ID", Name = "بطاقة الهوية" },
                new PersonIdentityTypeName { Id = "ID_EN", LanguageId = "EN", PersonIdentityTypeId = "ID", Name = "Identity Card" },

                new PersonIdentityTypeName { Id = "DL_FR", LanguageId = "FR", PersonIdentityTypeId = "DL", Name = "Permis de conduire" },
                new PersonIdentityTypeName { Id = "DL_AR", LanguageId = "AR", PersonIdentityTypeId = "DL", Name = "رخصة القيادة" },
                new PersonIdentityTypeName { Id = "DL_EN", LanguageId = "EN", PersonIdentityTypeId = "DL", Name = "Driver's License" },

                new PersonIdentityTypeName { Id = "RC_FR", LanguageId = "FR", PersonIdentityTypeId = "RC", Name = "Carte de Séjour" },
                new PersonIdentityTypeName { Id = "RC_AR", LanguageId = "AR", PersonIdentityTypeId = "RC", Name = "بطاقة الإقامة" },
                new PersonIdentityTypeName { Id = "RC_EN", LanguageId = "EN", PersonIdentityTypeId = "RC", Name = "Residence Card" }
                );
        }
    }
}
