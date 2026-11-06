using CruxIT.Library.GeoLocations;
using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
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
                FullNumber = CxPhoneHelper.GetPhoneNumberInterNationalFormat(Shared.SharedValues.SuperAdminPhone, "TN"),
                Number = CxPhoneHelper.GetPhoneNumberNationalFormat(Shared.SharedValues.SuperAdminPhone, "TN"),
                PhoneTypeId = "Mob",
                IsValid = true,
                PersonBaseId = Shared.SharedValues.SuperAdminUserId,
                CreatedDate = Shared.SharedValues.DefaultCreatedDate,
            });
        }
    }
}
