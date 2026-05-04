using CruxSuits.Infrastructure.EntityFramework.Entities.Banks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework.Configurations.Banks
{
    internal class IBANConfiguration : IEntityTypeConfiguration<IBAN>
    {
        public void Configure(EntityTypeBuilder<IBAN> builder)
        {

        }
    }
}
