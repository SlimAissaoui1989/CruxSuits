using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests
{
    [Collection("SharedDb")]
    public abstract class BaseTests 
    {
        protected SharedCruxSuitsSqliteFixture Fixture { get; }

        public BaseTests(SharedCruxSuitsSqliteFixture fixture)
        {
            Fixture = fixture;
        }
    }
}
