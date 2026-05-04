using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Banks;
using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Fields;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.Datas.Objects;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.EntityFramework
{
    public class CruxSuitsContext : CxDbContext
    {
        public CruxSuitsContext()
            : base()
        {
        }

        public CruxSuitsContext(DbContextOptions options)
            : base(options)
        {
        }

        public CruxSuitsContext(string connectionString)
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
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=CXSuitsDB;User ID=sa;Password=123;Encrypt=True;TrustServerCertificate=True;", option =>
                {
                    option.UseCompatibilityLevel(120);
                });
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        //Banks
        public DbSet<IBAN> IBANs { get; set; }


        //Contacts
        public DbSet<EMail> EMails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<PhoneTypeName> PhoneTypeNames { get; set; }


        //Files
        public DbSet<DigitalFile> DigitalFiles { get; set; }
        public DbSet<DigitalFileTrace> DigitalFileTraces { get; set; }


        //GeoLocations
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryName> CountryNames { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyName> CurrencyNames { get; set; }


        //Languages
        public DbSet<Language> Languages { get; set; }


        //Permissions
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        //Permissions.Datas.Fields
        public DbSet<DataField> DataFields { get; set; }
        public DbSet<DataFieldGeneralPermission> DataFieldGeneralPermissions { get; set; }
        public DbSet<DataFieldPermissionByRole> DataFieldPermissionByRoles { get; set; }
        public DbSet<DataFieldPermissionByUser> DataFieldPermissionByUsers { get; set; }
        public DbSet<DataFieldPermissionByUserDetail> DataFieldPermissionByUserDetails { get; set; }


        //Permissions.Datas.Object
        public DbSet<DataObject> DataObjects { get; set; }
        public DbSet<DataObjectGeneralPermission> DataObjectGeneralPermissions { get; set; }
        public DbSet<DataObjectPermissionByRole> DataObjectPermissionByRoles { get; set; }
        public DbSet<DataObjectPermissionByUser> DataObjectPermissionByUsers { get; set; }
        public DbSet<DataObjectPermissionByUserDetail> DataObjectPermissionByUserDetails { get; set; }


        //Permission.UiResources
        public DbSet<RoleUiResourcePermission> RoleUiResourcePermissions { get; set; }
        public DbSet<UiResource> UiResources { get; set; }
        public DbSet<UiResourceName> UiResourceNames { get; set; }
        public DbSet<UiResourceType> UiResourceTypes { get; set; }
        public DbSet<UiResourceTypeName> UiResourceTypeNames { get; set; }
        public DbSet<UserUiResourcePermission> UserUiResourcePermissions { get; set; }


        //Persons
        public DbSet<Gender> Genders { get; set; }
        public DbSet<GenderName> GenderNames { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonIdentity> PersonIdentities { get; set; }
        public DbSet<PersonIdentityType> PersonIdentityTypes { get; set; }
        public DbSet<PersonIdentityTypeName> PersonIdentityTypeNames { get; set; }
    }
}
