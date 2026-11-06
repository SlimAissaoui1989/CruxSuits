using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PersonEntity = CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities.Person;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasData(
                new PersonEntity
                {
                    FirstName = Shared.SharedValues.SuperAdminFirstName,
                    LastName = Shared.SharedValues.SuperAdminLastName,
                    FullName = Shared.SharedValues.SuperAdminFullName,
                    Id = Shared.SharedValues.SuperAdminUserId,
                    CreatedDate = Shared.SharedValues.DefaultCreatedDate,
                    IsDeleted = false,
                });
        }
    }
}
