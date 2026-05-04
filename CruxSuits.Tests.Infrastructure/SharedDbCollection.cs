using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure
{
    [CollectionDefinition("SharedDb")]
    public class SharedDbCollection : ICollectionFixture<SharedCruxSuitsSqliteFixture>
    {
        // no code inside; this just ties the fixture to the collection
    }
}
