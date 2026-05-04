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
    internal class PersonIdentityConfiguration : IEntityTypeConfiguration<PersonIdentity>
    {
        public void Configure(EntityTypeBuilder<PersonIdentity> builder)
        {
            builder.HasIndex(x => x.FullNumber).IsUnique();
        }
    }
}
