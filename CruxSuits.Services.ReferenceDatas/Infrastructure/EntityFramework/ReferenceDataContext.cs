using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.BaseType is null && InheritsSoftDeleteEntity(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "entity");
                    var isDeleted = Expression.Property(parameter, nameof(SoftDeleteEntity<string>.IsDeleted));
                    var filter = Expression.Lambda(Expression.Not(isDeleted), parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }

        private static bool InheritsSoftDeleteEntity(Type type)
        {
            for (var current = type; current is not null; current = current.BaseType)
            {
                if (current.IsGenericType &&
                    current.GetGenericTypeDefinition() == typeof(SoftDeleteEntity<>))
                {
                    return true;
                }
            }

            return false;
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

        public DbSet<AdministrativeArea> AdministrativeAreas { get; set; } = null!;
        public DbSet<AdministrativeAreaName> AdministrativeAreaNames { get; set; } = null!;
        public DbSet<Locality> Localities { get; set; } = null!;
        public DbSet<LocalityName> LocalityNames { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<CountryName> CountryNames { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;
        public DbSet<CurrencyName> CurrencyNames { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
    }
}
