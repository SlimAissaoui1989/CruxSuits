using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Tests.Infrastructure.Tests;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Utilities.EntityFramework
{
    public class SharedCruxSuitsSqliteFixture 
    {
        public async Task<CruxSuitsUnitOfWork> CreateUnitOfWorkAsync()
        {
            CruxSuitsTestContext context = new CruxSuitsTestContext();
            await context.Database.EnsureCreatedAsync();
            return new CruxSuitsUnitOfWork(context);
        }
    }
}
