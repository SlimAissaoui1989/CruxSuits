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
    internal class PersonBaseConfiguration : IEntityTypeConfiguration<PersonBase>
    {
        public void Configure(EntityTypeBuilder<PersonBase> builder)
        {
            builder.HasQueryFilter(f => !f.IsDeleted);
        }
    }
}
