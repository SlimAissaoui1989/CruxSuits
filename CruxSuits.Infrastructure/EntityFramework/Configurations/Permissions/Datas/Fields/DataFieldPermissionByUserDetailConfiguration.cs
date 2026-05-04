using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Permissions.Datas.Fields
{
    internal class DataFieldPermissionByUserDetailConfiguration : IEntityTypeConfiguration<DataFieldPermissionByUserDetail>
    {
        public void Configure(EntityTypeBuilder<DataFieldPermissionByUserDetail> builder)
        {
            builder.HasIndex(i => new { i.DataFieldPermissionByUserId, i.RecordId }).IsUnique();
        }
    }
}
