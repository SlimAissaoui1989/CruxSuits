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
    internal class PersonIdentityTypeConfiguration : IEntityTypeConfiguration<PersonIdentityType>
    {
        public void Configure(EntityTypeBuilder<PersonIdentityType> builder)
        {
            DateTime now = new DateTime(2025, 01, 01);

            builder.HasQueryFilter(f => !f.IsDeleted);

            builder.HasData(
                new PersonIdentityType { Id = "PASS", CreatedUserId = 0, CreatedUserName = "system", CreatedDate = now, IsDeleted = false},
                new PersonIdentityType { Id = "ID", CreatedUserId = 0, CreatedUserName = "system", CreatedDate = now, IsDeleted = false },
                new PersonIdentityType { Id = "DL", CreatedUserId = 0, CreatedUserName = "system", CreatedDate = now, IsDeleted = false },
                new PersonIdentityType { Id = "RC", CreatedUserId = 0, CreatedUserName = "system", CreatedDate = now, IsDeleted = false }
                );
        }
    }
}
