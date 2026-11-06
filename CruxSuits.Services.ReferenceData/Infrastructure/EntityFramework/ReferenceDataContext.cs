using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework
{
    public class ReferenceDataContext : CxDbContext
    {
        public ReferenceDataContext()
            : base()
        {
        }

        public ReferenceDataContext(DbContextOptions options)
            : base(options)
        {
        }

        public ReferenceDataContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=ReferenceDataDB;User ID=sa;Password=123;Encrypt=True;TrustServerCertificate=True;",
                    sqlOptions => sqlOptions.UseCompatibilityLevel(120));
            }

            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<LocalizedName> LocalizedNames { get; set; } = null!;
    }
}
