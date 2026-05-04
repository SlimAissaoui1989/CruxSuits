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
    internal class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneType>
    {
        public void Configure(EntityTypeBuilder<PhoneType> builder)
        {
            DateTime now = new DateTime(2025, 01, 01);

            builder.HasQueryFilter(f => !f.IsDeleted);

            builder.HasData(
                new PhoneType { Id = "Mob", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },
                new PhoneType { Id = "Hom", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },
                new PhoneType { Id = "Wrk", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false },
                new PhoneType { Id = "Fax", CreatedDate = now, CreatedUserId = 0, CreatedUserName = "system", IsDeleted = false }
                );
        }
    }
}
