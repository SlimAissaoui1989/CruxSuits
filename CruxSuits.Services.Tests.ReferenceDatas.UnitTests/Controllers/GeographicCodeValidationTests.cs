using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class GeographicCodeValidationTests
{
    // Contract check: verifies area and locality requests accept the supported code lengths;
    // A pass confirms longer uppercase codes and existing codes remain valid;
    // Failure signals a regression in code validation.
    [Theory]
    [InlineData("A", false, false)]
    [InlineData("SF", true, false)]
    [InlineData("BRD", true, true)]
    [InlineData("ABCD", false, true)]
    [InlineData("ABCDE", false, false)]
    [InlineData("A1B", false, false)]
    [InlineData("abc", false, false)]
    public void Code_ValidatesLengthAndUppercaseLetters(string code, bool areaExpected, bool localityExpected)
    {
        var area = new AdministrativeAreaRequest { Code = code };
        var locality = new LocalityRequest { Code = code };

        Assert.Equal(areaExpected, Validator.TryValidateObject(area, new ValidationContext(area), null, true));
        Assert.Equal(localityExpected, Validator.TryValidateObject(locality, new ValidationContext(locality), null, true));
    }
}
