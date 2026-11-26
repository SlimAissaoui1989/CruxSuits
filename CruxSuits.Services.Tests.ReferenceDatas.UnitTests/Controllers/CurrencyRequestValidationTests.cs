using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Controllers;

public sealed class CurrencyRequestValidationTests
{
    // Contract check: verifies create and update accept only a period or comma as the decimal separator;
    // A pass confirms both requests enforce the same allowed values;
    // Failure means a valid separator was rejected or an invalid separator was accepted.
    [Theory]
    [InlineData('.', true)]
    [InlineData(',', true)]
    [InlineData('a', false)]
    [InlineData('1', false)]
    [InlineData(' ', false)]
    [InlineData('\0', false)]
    public void DecimalSeparator_WhenValidated_AllowsOnlyPeriodOrComma(char separator, bool expected)
    {
        object[] requests =
        [
            new CurrencyRequest { DecimalSeparator = separator },
            new UpdateCurrencyRequest { DecimalSeparator = separator }
        ];

        Assert.All(requests, request =>
        {
            var context = new ValidationContext(request) { MemberName = nameof(CurrencyRequest.DecimalSeparator) };
            Assert.Equal(expected, Validator.TryValidateProperty(separator, context, null));
        });
    }
}
