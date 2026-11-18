using Xunit;

namespace CruxSuits.Services.Files.Tests.IntegrationTests;

[CollectionDefinition(Name, DisableParallelization = true)]
public sealed class FilesServiceIntegrationCollection
    : ICollectionFixture<FilesServiceIntegrationFixture>
{
    public const string Name = "Files service integration";
}
