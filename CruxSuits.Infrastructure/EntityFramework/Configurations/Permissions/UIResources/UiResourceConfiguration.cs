using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Permissions.UIResources
{
    internal class UiResourceConfiguration : IEntityTypeConfiguration<UiResource>
    {
        public void Configure(EntityTypeBuilder<UiResource> builder)
        {
            builder.HasIndex(i => i.Key).IsUnique();
        }
    }
}
