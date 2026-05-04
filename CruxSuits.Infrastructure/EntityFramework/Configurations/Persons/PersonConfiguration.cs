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
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasOne(p => p.User).WithOne(u => u.Person);

            builder.HasData(
                new Person
                {
                    FirstName = "Slim",
                    LastName = "Aissaoui",
                    FullName = "Slim Aissaoui",
                    Id = 1,
                    CreatedDate = new DateTime(2025, 01, 01),
                    CreatedUserId = 0,
                    CreatedUserName = "system",
                    IsDeleted = false,
                });
        }
    }
}
