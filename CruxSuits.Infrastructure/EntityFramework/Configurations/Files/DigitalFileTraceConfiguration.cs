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
    internal class DigitalFileTraceConfiguration : IEntityTypeConfiguration<DigitalFileTrace>
    {
        public void Configure(EntityTypeBuilder<DigitalFileTrace> builder)
        {
            builder.HasIndex(i => i.DigitalFileId);
        }
    }
}
