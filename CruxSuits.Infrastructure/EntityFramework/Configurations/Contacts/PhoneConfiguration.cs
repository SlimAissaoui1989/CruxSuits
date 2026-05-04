using CruxIT.Library.GeoLocations;
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
    internal class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasIndex(i => i.FullNumber).IsUnique();
            builder.HasData(new Phone
            {
                Id = 1,
                CountryId = "TN",
                FullNumber = CxPhoneHelper.GetPhoneNumberInterNationalFormat("58 890 780", "TN"),
                Number = CxPhoneHelper.GetPhoneNumberNationalFormat("58 890 780","TN"),
                PhoneTypeId = "Mob",
                IsValid = true,
                PersonBaseId = 1,
                CreatedDate = new DateTime(2025, 01, 01),
                CreatedUserId = 0,
                CreatedUserName = "system",
            });
        }
    }
}
