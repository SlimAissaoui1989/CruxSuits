using CruxSuits.Services.Files.Tests.IntegrationTests;
using Reqnroll;

namespace CruxSuits.Services.Tests.Files.BddTests;

[Binding]
public static class FilesBddTestRunHooks
{
    public static FilesServiceIntegrationFixture Fixture { get; private set; } = null!;

    [BeforeTestRun]
    public static async Task StartFilesService()
    {
        Fixture = new FilesServiceIntegrationFixture();
        await Fixture.InitializeAsync();
    }

    [AfterTestRun]
    public static async Task StopFilesService()
    {
        if (Fixture is not null)
            await Fixture.DisposeAsync();
    }
}
