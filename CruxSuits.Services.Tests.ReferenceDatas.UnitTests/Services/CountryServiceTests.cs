using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class CountryServiceTests
{
    private readonly Mock<IReferenceDataUnitOfWork> _unitOfWork = new();
    private readonly Mock<ICountryRepository> _countryRepository = new();
    private readonly Mock<ICurrencyRepository> _currencyRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly CountryService _service;

    public CountryServiceTests()
    {
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Country).Returns(_countryRepository.Object);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Currency).Returns(_currencyRepository.Object);
        _service = new CountryService(_unitOfWork.Object, _mapper.Object);
    }

    #region GetAllAsync

    // Success case: verifies GetAllAsync returns countries for requested language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_ReturnsCountriesForRequestedLanguage()
    {
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var countries = new List<CountryResponse>
        {
            new() { Id = "US", Name = "United States", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" },
            new() { Id = "CA", Name = "Canada", Alpha3Code = "CAN", NumericCode = "124", CurrencyId = "CAD" }
        };
        _countryRepository
            .Setup(repository => repository.GetAllLocalizedAsync<CountryResponse>(languageId, cancellation.Token))
            .ReturnsAsync(countries);

        var result = await _service.GetAllAsync(languageId, cancellation.Token);

        Assert.Same(countries, result);
        _countryRepository.Verify(repository => repository.GetAllLocalizedAsync<CountryResponse>(languageId, cancellation.Token), Times.Once);
    }

    // Success case: verifies GetAllAsync returns empty list when repository has no countries;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetAllAsync_ReturnsEmptyListWhenRepositoryHasNoCountries()
    {
        var countries = new List<CountryResponse>();
        _countryRepository
            .Setup(repository => repository.GetAllLocalizedAsync<CountryResponse>(
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(countries);

        var result = await _service.GetAllAsync("en");

        Assert.Same(countries, result);
    }

    #endregion

    #region GetPhoneCodeOptionsAsync

    // Success case: verifies GetPhoneCodeOptionsAsync returns phone code options for requested language;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetPhoneCodeOptionsAsync_ReturnsPhoneCodeOptionsForRequestedLanguage()
    {
        const string languageId = "en";
        using var cancellation = new CancellationTokenSource();
        var options = new List<CountryPhoneCodeResponse>
        {
            new() { Id = "US", Name = "United States", PhoneCode = "+1" },
            new() { Id = "CA", Name = "Canada", PhoneCode = "+1" }
        };
        _countryRepository
            .Setup(repository => repository.GetAllLocalizedAsync<CountryPhoneCodeResponse>(languageId, cancellation.Token))
            .ReturnsAsync(options);

        var result = await _service.GetPhoneCodeOptionsAsync(languageId, cancellation.Token);

        Assert.Same(options, result);
        _countryRepository.Verify(repository => repository.GetAllLocalizedAsync<CountryPhoneCodeResponse>(languageId, cancellation.Token), Times.Once);
    }

    // Success case: verifies GetPhoneCodeOptionsAsync returns empty list when repository has no options;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetPhoneCodeOptionsAsync_ReturnsEmptyListWhenRepositoryHasNoOptions()
    {
        var options = new List<CountryPhoneCodeResponse>();
        _countryRepository
            .Setup(repository => repository.GetAllLocalizedAsync<CountryPhoneCodeResponse>(
                It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(options);

        var result = await _service.GetPhoneCodeOptionsAsync("en");

        Assert.Same(options, result);
    }

    #endregion

    #region GetByIdAsync

    // Success case: verifies GetByIdAsync returns country when found;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task GetByIdAsync_ReturnsCountryWhenFound()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var country = new CountryDetailsResponse
        {
            Id = countryId,
            Alpha3Code = "USA",
            NumericCode = "840",
            PhoneCode = "+1",
            CurrencyId = "USD"
        };
        var countryRepository = new Mock<ICountryRepository>();
        countryRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<CountryDetailsResponse>(countryId, cancellation.Token))
            .ReturnsAsync(country);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Country).Returns(countryRepository.Object);

        var result = await _service.GetByIdAsync(countryId, cancellation.Token);

        Assert.Same(country, result);
        countryRepository.Verify(repository => repository.GetByIdOrDefaultAsync<CountryDetailsResponse>(
            countryId, cancellation.Token), Times.Once);
    }

    // Failure case: verifies GetByIdAsync throws not found when country does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task GetByIdAsync_ThrowsNotFoundWhenCountryDoesNotExist()
    {
        const string countryId = "ZZ";
        var countryRepository = new Mock<ICountryRepository>();
        countryRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync<CountryDetailsResponse>(countryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CountryDetailsResponse?)null);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Country).Returns(countryRepository.Object);

        await Assert.ThrowsAsync<CxException>(() => _service.GetByIdAsync(countryId));
        countryRepository.Verify(repository => repository.GetByIdOrDefaultAsync<CountryDetailsResponse>(
            countryId, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region CreateAsync

    // Success case: verifies CreateAsync maps adds saves and returns country response;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task CreateAsync_MapsAddsSavesAndReturnsCountryResponse()
    {
        using var cancellation = new CancellationTokenSource();
        var request = new CountryRequest
        {
            Id = "US",
            Alpha3Code = "USA",
            NumericCode = "840",
            PhoneCode = "+1",
            CurrencyId = "USD"
        };
        var country = new Country
        {
            Id = request.Id,
            Alpha3Code = request.Alpha3Code,
            NumericCode = request.NumericCode,
            PhoneCode = request.PhoneCode,
            CurrencyId = request.CurrencyId
        };
        var response = new CountryResponse
        {
            Id = country.Id,
            Alpha3Code = country.Alpha3Code,
            NumericCode = country.NumericCode,
            PhoneCode = country.PhoneCode,
            CurrencyId = country.CurrencyId
        };
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(country.CurrencyId, cancellation.Token))
            .ReturnsAsync(new Currency { Id = country.CurrencyId });
        _mapper.Setup(mapper => mapper.Map<Country>(request)).Returns(country);
        _countryRepository.Setup(repository => repository.AddAsync(country, cancellation.Token)).ReturnsAsync(country);
        _mapper.Setup(mapper => mapper.Map<CountryResponse>(country)).Returns(response);

        var result = await _service.CreateAsync(request, cancellation.Token);

        Assert.Same(response, result);
        _mapper.Verify(mapper => mapper.Map<Country>(request), Times.Once);
        _countryRepository.Verify(repository => repository.AddAsync(country, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _mapper.Verify(mapper => mapper.Map<CountryResponse>(country), Times.Once);
    }

    #endregion

    #region UpdateAsync

    // Success case: verifies UpdateAsync maps updates saves and returns country response;
    // A pass confirms the expected result;
    // Failure signals a behavior regression.
    [Fact]
    public async Task UpdateAsync_MapsUpdatesSavesAndReturnsCountryResponse()
    {
        const string countryId = "US";
        using var cancellation = new CancellationTokenSource();
        var country = new Country
        {
            Id = countryId,
            Alpha3Code = "USA",
            NumericCode = "840",
            CurrencyId = "USD"
        };
        var request = new UpdateCountryRequest { PhoneCode = "+1" };
        var response = new CountryResponse
        {
            Id = country.Id,
            Alpha3Code = country.Alpha3Code,
            NumericCode = country.NumericCode,
            PhoneCode = request.PhoneCode,
            CurrencyId = country.CurrencyId
        };
        _countryRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(countryId, cancellation.Token))
            .ReturnsAsync(country);
        _currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(country.CurrencyId, cancellation.Token))
            .ReturnsAsync(new Currency { Id = country.CurrencyId });
        _mapper.Setup(mapper => mapper.Map(request, country)).Returns(country);
        _countryRepository.Setup(repository => repository.UpdateAsync(country, cancellation.Token)).ReturnsAsync(country);
        _mapper.Setup(mapper => mapper.Map<CountryResponse>(country)).Returns(response);

        var result = await _service.UpdateAsync(countryId, request, cancellation.Token);

        Assert.Same(response, result);
        _countryRepository.Verify(repository => repository.GetByIdOrDefaultAsync(countryId, cancellation.Token), Times.Once);
        _mapper.Verify(mapper => mapper.Map(request, country), Times.Once);
        _countryRepository.Verify(repository => repository.UpdateAsync(country, cancellation.Token), Times.Once);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(cancellation.Token), Times.Once);
        _mapper.Verify(mapper => mapper.Map<CountryResponse>(country), Times.Once);
    }

    // Failure case: verifies UpdateAsync throws not found when country does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_ThrowsNotFoundWhenCountryDoesNotExist()
    {
        const string countryId = "ZZ";
        var request = new UpdateCountryRequest { PhoneCode = "+1" };
        _countryRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(countryId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Country?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync(countryId, request));

        _countryRepository.Verify(repository => repository.GetByIdOrDefaultAsync(countryId, It.IsAny<CancellationToken>()), Times.Once);
        _mapper.Verify(mapper => mapper.Map(It.IsAny<UpdateCountryRequest>(), It.IsAny<Country>()), Times.Never);
        _countryRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region CreateAsyncValidation

    // Failure case: verifies CreateAsync throws without adding when country data is invalid;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Theory]
    [InlineData("USA", "USA", "840", "USD")]
    [InlineData("US", "US", "840", "USD")]
    [InlineData("US", "USA", "84A", "USD")]
    [InlineData("US", "USA", "840", " ")]
    public async Task CreateAsync_WhenCountryDataIsInvalid_ThrowsWithoutAdding(
        string id, string alpha3Code, string numericCode, string currencyId)
    {
        var request = new CountryRequest { Id = id, Alpha3Code = alpha3Code, NumericCode = numericCode, CurrencyId = currencyId };
        var country = new Country { Id = id, Alpha3Code = alpha3Code, NumericCode = numericCode, CurrencyId = currencyId };
        _mapper.Setup(mapper => mapper.Map<Country>(request)).Returns(country);

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request));

        _countryRepository.Verify(repository => repository.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync throws without adding when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenCurrencyDoesNotExist_ThrowsWithoutAdding()
    {
        var request = new CountryRequest { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "ZZZ" };
        _mapper.Setup(mapper => mapper.Map<Country>(request)).Returns(new Country
        {
            Id = request.Id, Alpha3Code = request.Alpha3Code, NumericCode = request.NumericCode, CurrencyId = request.CurrencyId
        });
        _currencyRepository.Setup(repository => repository.GetByIdOrDefaultAsync("ZZZ", It.IsAny<CancellationToken>()))
            .ReturnsAsync((Currency?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request));

        _countryRepository.Verify(repository => repository.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync throws without adding when country id already exists;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task CreateAsync_WhenCountryIdAlreadyExists_ThrowsWithoutAdding()
    {
        var request = new CountryRequest { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" };
        var country = new Country { Id = request.Id, Alpha3Code = request.Alpha3Code, NumericCode = request.NumericCode, CurrencyId = request.CurrencyId };
        _mapper.Setup(mapper => mapper.Map<Country>(request)).Returns(country);
        _countryRepository.Setup(repository => repository.GetByIdOrDefaultAsync("US", It.IsAny<CancellationToken>())).ReturnsAsync(country);

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request));

        _countryRepository.Verify(repository => repository.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region UpdateAsyncValidation

    // Failure case: verifies UpdateAsync throws without updating when country code is invalid;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Theory]
    [InlineData("US", "840", "USD")]
    [InlineData("USA", "84A", "USD")]
    [InlineData("USA", "840", " ")]
    public async Task UpdateAsync_WhenCountryCodeIsInvalid_ThrowsWithoutUpdating(string alpha3Code, string numericCode, string currencyId)
    {
        var existing = new Country { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" };
        var request = new UpdateCountryRequest { Alpha3Code = alpha3Code, NumericCode = numericCode, CurrencyId = currencyId };
        _countryRepository.Setup(repository => repository.GetByIdOrDefaultAsync("US", It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        _mapper.Setup(mapper => mapper.Map(request, existing)).Callback(() =>
        {
            existing.Alpha3Code = alpha3Code;
            existing.NumericCode = numericCode;
            existing.CurrencyId = currencyId;
        });

        await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync("US", request));

        _countryRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies CreateAsync throws conflict without adding when country code is already assigned;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Theory]
    [InlineData("USA", "124")]
    [InlineData("CAN", "840")]
    public async Task CreateAsync_WhenCountryCodeIsAlreadyAssigned_ThrowsConflictWithoutAdding(string alpha3Code, string numericCode)
    {
        var request = new CountryRequest { Id = "CA", Alpha3Code = alpha3Code, NumericCode = numericCode, CurrencyId = "USD" };
        var country = new Country { Id = "CA", Alpha3Code = alpha3Code, NumericCode = numericCode, CurrencyId = "USD" };
        var existing = new Country { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" };
        _mapper.Setup(mapper => mapper.Map<Country>(request)).Returns(country);
        _currencyRepository.Setup(repository => repository.GetByIdOrDefaultAsync("USD", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Currency { Id = "USD" });
        _countryRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<Country, bool>> predicate, CancellationToken _) => predicate.Compile()(existing) ? existing : null);

        await Assert.ThrowsAsync<CxException>(() => _service.CreateAsync(request));

        _countryRepository.Verify(repository => repository.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies UpdateAsync throws conflict without updating when country code is already assigned;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Theory]
    [InlineData("USA", "124")]
    [InlineData("CAN", "840")]
    public async Task UpdateAsync_WhenCountryCodeIsAlreadyAssigned_ThrowsConflictWithoutUpdating(string alpha3Code, string numericCode)
    {
        var country = new Country { Id = "CA", Alpha3Code = "CAN", NumericCode = "124", CurrencyId = "USD" };
        var otherCountry = new Country { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" };
        var request = new UpdateCountryRequest { Alpha3Code = alpha3Code, NumericCode = numericCode, CurrencyId = "USD" };
        _countryRepository.Setup(repository => repository.GetByIdOrDefaultAsync("CA", It.IsAny<CancellationToken>())).ReturnsAsync(country);
        _mapper.Setup(mapper => mapper.Map(request, country)).Callback(() =>
        {
            country.Alpha3Code = alpha3Code;
            country.NumericCode = numericCode;
        });
        _currencyRepository.Setup(repository => repository.GetByIdOrDefaultAsync("USD", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Currency { Id = "USD" });
        _countryRepository
            .Setup(repository => repository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Expression<Func<Country, bool>> predicate, CancellationToken _) => predicate.Compile()(otherCountry) ? otherCountry : null);

        await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync("CA", request));

        _countryRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    // Failure case: verifies UpdateAsync throws without updating when currency does not exist;
    // A pass confirms the invalid or missing case is handled;
    // Failure means it was accepted or mishandled.
    [Fact]
    public async Task UpdateAsync_WhenCurrencyDoesNotExist_ThrowsWithoutUpdating()
    {
        var existing = new Country { Id = "US", Alpha3Code = "USA", NumericCode = "840", CurrencyId = "USD" };
        var request = new UpdateCountryRequest { Alpha3Code = "USA", NumericCode = "840", CurrencyId = "ZZZ" };
        _countryRepository.Setup(repository => repository.GetByIdOrDefaultAsync("US", It.IsAny<CancellationToken>())).ReturnsAsync(existing);
        _mapper.Setup(mapper => mapper.Map(request, existing)).Callback(() => existing.CurrencyId = "ZZZ");
        _currencyRepository.Setup(repository => repository.GetByIdOrDefaultAsync("ZZZ", It.IsAny<CancellationToken>())).ReturnsAsync((Currency?)null);

        await Assert.ThrowsAsync<CxException>(() => _service.UpdateAsync("US", request));

        _countryRepository.Verify(repository => repository.UpdateAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(unitOfWork => unitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion
}
