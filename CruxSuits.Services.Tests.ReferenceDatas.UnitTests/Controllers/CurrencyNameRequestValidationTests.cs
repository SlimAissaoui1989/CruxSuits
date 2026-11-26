using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class CurrencyNameRequestValidationTests
{
    // Contract check: verifies the create and update requests apply the same country and currency name rule;
    // A pass confirms that names with letters and word spaces are accepted;
    // Failure signals that a valid name was rejected or an invalid name was accepted.
    [Theory]
    [InlineData("Euro", true)]
    [InlineData("US Dollar", true)]
    [InlineData("dollar des États-Unis", false)]
    [InlineData("dollar des États Unis", true)]
    [InlineData("دولار أمريكي", true)]
    [InlineData("Dollar2", false)]
    [InlineData("Dollar$", false)]
    [InlineData("Dollar\tUS", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    public void Name_WhenValidated_AppliesTheSameRuleToCreateAndUpdate(string name, bool expected)
    {
        object[] requests =
        [
            new CurrencyNameRequest { LanguageId = "en", Name = name },
            new CurrencyNameUpdateRequest { Name = name },
            new CountryNameRequest { LanguageId = "en", Name = name, Nationality = "American" },
            new CountryNameUpdateRequest { Name = name, Nationality = "American" }
        ];

        Assert.All(requests, request =>
        {
            var context = new ValidationContext(request) { MemberName = "Name" };
            Assert.Equal(expected, Validator.TryValidateProperty(name, context, null));
        });
    }
}
