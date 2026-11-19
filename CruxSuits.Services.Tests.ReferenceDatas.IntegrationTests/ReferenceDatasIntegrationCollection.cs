using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.IntegrationTests;

[CollectionDefinition(Name, DisableParallelization = true)]
public sealed class ReferenceDatasIntegrationCollection : ICollectionFixture<ReferenceDatasIntegrationFixture>
{
    public const string Name = "Reference datas integration";
}
