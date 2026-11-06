using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Services.Files.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.Files.Infrastructure.EntityFramework
{
    public class FilesContext : CxDbContext
    {
        public FilesContext()
            : base()
        {
            ConfigureAutoTransactionBehavior();
        }

        public FilesContext(DbContextOptions options)
            : base(options)
        {
            ConfigureAutoTransactionBehavior();
        }

        public FilesContext(string connectionString)
            : base(connectionString)
        {
            ConfigureAutoTransactionBehavior();
        }

        private void ConfigureAutoTransactionBehavior()
        {
            Database.AutoTransactionBehavior = AutoTransactionBehavior.Always;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=localhost;Initial Catalog=FilesDB;User ID=sa;Password=123;Encrypt=True;TrustServerCertificate=True;",
                    sqlOptions => sqlOptions.UseCompatibilityLevel(120));
            }

            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        public DbSet<UploadedFile> UploadedFiles { get; set; } = null!;

        public DbSet<UploadedFileTrace> UploadedFileTraces { get; set; } = null!;
    }
}
