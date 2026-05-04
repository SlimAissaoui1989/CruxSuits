using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.EntityFramework
{
    public class CruxSuitsContextTests : BaseTests
    {
        CruxSuitsTestContext _context;

        public CruxSuitsContextTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
            _context = new CruxSuitsTestContext();
        }

        [Fact]
        public void All_ForeignKeys_Should_Have_Restrict_DeleteBehavior_Async()
        {
            var fks = _context.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .ToList();

            fks.Should().NotBeEmpty("the model should contain relationships");

            foreach (var fk in fks)
            {
                fk.DeleteBehavior.Should().Be(DeleteBehavior.Restrict,
                    $"FK {fk.DeclaringEntityType.DisplayName()} -> {fk.PrincipalEntityType.DisplayName()} must be Restrict");
            }
        }

        [Fact]
        public void Context_Should_Be_Able_To_Build_Model_And_Open_Connection_Async()
        {
            // Force model building & a quick roundtrip to DB.
            _context.Database.CanConnect().Should().BeTrue();
        }

        [Fact]
        public void All_Tables_And_Views_Should_Be_Exist_And_Not_Duplicated_In_RentalDbContext()
        {
            IEnumerable<Type> tables = GetAllClassesUnderNamespace(_context.GetType().Assembly, "Rental.DataBase.EntitiyFramework.Tables");
            IEnumerable<Type> views = GetAllClassesUnderNamespace(_context.GetType().Assembly, "Rental.DataBase.EntitiyFramework.Views");
            IEnumerable<Type> dbSets = GetAllEntityTypes(_context);

            IEnumerable<Type> missingTables = tables.Where(c => !dbSets.Contains(c));
            IEnumerable<Type> duplicatedTables = dbSets.GroupBy(g => g).Where(c => c.Count() > 1).SelectMany(s => s);

            IEnumerable<Type> missingViews = views.Where(c => !dbSets.Contains(c));
            IEnumerable<Type> duplicatedViews = dbSets.GroupBy(g => g).Where(c => c.Count() > 1).SelectMany(s => s);

            missingTables.Should().BeEmpty(
                because: "every class in the Tables namespace must be mapped into the EF model. Missing: {0}",
                string.Join(", ", missingTables.Distinct().Select(s => s.Name).ToArray()));

            duplicatedTables.Should().BeEmpty(
                because: "no duplicate entity types are allowed. Duplicates: {0}",
                string.Join(", ", duplicatedTables.Distinct().Select(s => s.Name).ToArray()));

            missingViews.Should().BeEmpty(
                because: "every class in the Views namespace must be mapped into the EF model. Missing: {0}",
                string.Join(", ", missingViews.Distinct().Select(s => s.Name).ToArray()));

            duplicatedViews.Should().BeEmpty(
                because: "no duplicate entity types are allowed. Duplicates: {0}",
                string.Join(", ", duplicatedViews.Distinct().Select(s => s.Name).ToArray()));
        }

        private IEnumerable<Type> GetAllClassesUnderNamespace(Assembly assembly, string @namespace)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass
                            && t.Namespace != null && t.Namespace.StartsWith(@namespace));
        }
        private IEnumerable<Type> GetAllEntityTypes(DbContext context)
        {
            return context.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GetGenericArguments()[0]);
        }

        [Fact]
        public void All_Configurations_Should_Be_Exist()
        {

            IEnumerable<Type> configurations = GetAllClassesUnderNamespace(_context.GetType().Assembly, "Rental.DataBase.EntitiyFramework.Configurations")
                .Where(c => c.GetInterfaces().Any())
                .Select(s => s.GetInterfaces()[0])
                .Where(c => c.GetGenericArguments().Any())
                .Select(s => s.GetGenericArguments()[0]);
            IEnumerable<Type> tablesAndViews = GetAllClassesUnderNamespace(_context.GetType().Assembly, "Rental.DataBase.EntitiyFramework.Tables")
                .Concat(GetAllClassesUnderNamespace(_context.GetType().Assembly, "Rental.DataBase.EntitiyFramework.Views"));

            IEnumerable<Type> missingConfigurations = tablesAndViews.Where(c => !configurations.Contains(c));
            IEnumerable<Type> duplicatedConfigurations = configurations.GroupBy(g => g).Where(c => c.Count() > 1).SelectMany(s => s);

            missingConfigurations.Should().BeEmpty(
                because: "every class in the Table and Views namespace must be mapped into the Configurations namespace. Missing: {0}",
                string.Join(", ", missingConfigurations.Distinct().Select(s => s.Name).ToArray()));

            duplicatedConfigurations.Should().BeEmpty(
                because: "no duplicate entity types are allowed. Duplicates: {0}",
                string.Join(", ", duplicatedConfigurations.Distinct().Select(s => s.Name).ToArray()));
        }
    }
}
