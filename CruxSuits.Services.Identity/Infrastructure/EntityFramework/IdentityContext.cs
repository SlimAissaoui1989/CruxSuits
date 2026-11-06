using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.EntityFramework
{
    public class IdentityContext : CxDbContext
    {
        public IdentityContext()
            : base()
        {
        }

        public IdentityContext(DbContextOptions options)
            : base(options)
        {
        }

        public IdentityContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>()
                .Property(p => p.RowVersion)
                .IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Only configure the provider if the caller hasn't already done so
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=IdentityDB;User ID=sa;Password=123;Encrypt=True;TrustServerCertificate=True;", option =>
                {
                    option.UseCompatibilityLevel(120);
                });
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginIdentifier> UserLoginIdentifiers { get; set; }
    }
}
