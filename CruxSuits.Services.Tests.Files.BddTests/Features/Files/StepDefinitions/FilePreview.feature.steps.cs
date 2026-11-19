using System;
using Reqnroll;
using Xunit;

namespace CruxSuits.Services.Tests.Files.BddTests.Features.Files.StepDefinitions;

public sealed partial class FilesControllerSteps
{
    [Given("a public JPEG image exists")]
    public Task GivenPublicJpegImageExists()
    {
        _fileName = "bdd-image.jpg";
        _fileBytes = TestImageBytes;
        return CreateFileAsync(true, true);
    }

    [Given("a private JPEG image exists")]
    public Task GivenPrivateJpegImageExists()
    {
        _fileName = "bdd-image.jpg";
        _fileBytes = TestImageBytes;
        return CreateFileAsync(true, false);
    }

    [Given("a public ZIP archive exists")]
    public Task GivenPublicZipArchiveExists()
    {
        _fileName = "bdd-archive.zip";
        _fileBytes = TestArchiveBytes;
        return CreateFileAsync(true, true);
    }

    [Given("a private ZIP archive exists")]
    public Task GivenPrivateZipArchiveExists()
    {
        _fileName = "bdd-archive.zip";
        _fileBytes = TestArchiveBytes;
        return CreateFileAsync(true, false);
    }

    [When("an anonymous client previews the image")]
    [When("an anonymous client previews the archive")]
    public Task WhenAnonymousClientPreviewsFile() => PreviewAsync(isInternal: false, anonymous: true);

    [When("an authorized service previews the image through the internal route")]
    [When("an authorized service previews it through the internal route")]
    public Task WhenAuthorizedServicePreviewsFile() => PreviewAsync(isInternal: true, anonymous: false);

    [Then("the preview response status is (\\d+)")]
    public void ThenPreviewResponseStatusIs(int statusCode) => AssertStatus(statusCode);

    [Then("the image is returned inline with safe preview headers")]
    public async Task ThenImageIsInlineWithSafeHeaders()
    {
        AssertInlinePreview();
        Assert.Equal("nosniff", _response!.Headers.GetValues("X-Content-Type-Options").Single());
        Assert.Contains("sandbox", _response.Headers.GetValues("Content-Security-Policy").Single(), StringComparison.Ordinal);
        await AssertDownloadedBytesAsync();
    }

    [Then("the image is returned inline")]
    public async Task ThenImageIsReturnedInline()
    {
        AssertInlinePreview();
        await AssertDownloadedBytesAsync();
    }
}
