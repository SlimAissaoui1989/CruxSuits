using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.Person.Infrastructure.EntityFramework
{
    public class PersonContext : CxDbContext
    {
        public PersonContext()
            : base()
        {
        }

        public PersonContext(DbContextOptions options)
            : base(options)
        {
        }

        public PersonContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=PersonDB;User ID=sa;Password=123;Encrypt=True;TrustServerCertificate=True;",
                    option => option.UseCompatibilityLevel(120));
            }

            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CompanyContact> CompanyContacts { get; set; } = null!;
        public DbSet<CompanyLegalIdentity> CompanyLegalIdentities { get; set; } = null!;
        public DbSet<CompanyLegalIdentityType> CompanyLegalIdentityTypes { get; set; } = null!;
        public DbSet<EMail> EMails { get; set; } = null!;
        public DbSet<Gender> Genders { get; set; } = null!;
        public DbSet<IBAN> IBANs { get; set; } = null!;
        public DbSet<Person.Infrastructure.EntityFramework.Entities.Person> Persons { get; set; } = null!;
        public DbSet<PersonIdentity> PersonIdentities { get; set; } = null!;
        public DbSet<PersonIdentityType> PersonIdentityTypes { get; set; } = null!;
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<PhoneType> PhoneTypes { get; set; } = null!;
    }
}
