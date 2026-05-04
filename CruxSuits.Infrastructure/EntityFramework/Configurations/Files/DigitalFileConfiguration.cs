using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Files
{
    internal class DigitalFileConfiguration : IEntityTypeConfiguration<DigitalFile>
    {
        public void Configure(EntityTypeBuilder<DigitalFile> builder)
        {
            builder.HasQueryFilter(q => !q.IsDeleted);
        }
    }
}
