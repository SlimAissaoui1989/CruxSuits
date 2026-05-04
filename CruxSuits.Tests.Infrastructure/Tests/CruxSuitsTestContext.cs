using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests
{
    public class CruxSuitsTestContext : CruxSuitsContext
    {
        // The keeper connection makes the DB persist for the whole test run
        private static SqliteConnection? _keeper;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CruxSuitsContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        private static DbContextOptions<CruxSuitsContext> GetContextOptions()
        {
            string cnxStr = $"Data Source=file:CruxSuits-tests;mode=memory;cache=shared";
            //string cnxStr = $"Data Source=C:/Users/Slim Aissaoui/Desktop/database.db";
            _keeper = new SqliteConnection(cnxStr);
            _keeper.Open();

            return new DbContextOptionsBuilder<CruxSuitsContext>()
                .UseSqlite(cnxStr)                          // contexts can open/close their own connections
                .EnableSensitiveDataLogging()
                .Options;
        }
        public CruxSuitsTestContext()
            : base(GetContextOptions())
        {

        }
        public override void Dispose()
        {
            _keeper?.Dispose();
            base.Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(m => Console.WriteLine(m));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
