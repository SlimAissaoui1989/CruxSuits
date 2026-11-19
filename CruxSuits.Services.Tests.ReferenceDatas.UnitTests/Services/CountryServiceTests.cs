using System.Linq.Expressions;
using AutoMapper;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Services;
using CruxSuits.Shared.DTOs.ReferenceDatas;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Services;

public sealed class CountryServiceTests
{
    private readonly Mock<IReferenceDataUnitOfWork> _unitOfWork = new();
    private readonly Mock<ICountryNameRepository> _countryNameRepository = new();
    private readonly Mock<ICountryRepository> _countryRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly CountryService _service;

    public CountryServiceTests()
    {
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.CountryName).Returns(_countryNameRepository.Object);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Country).Returns(_countryRepository.Object);
        _service = new CountryService(_unitOfWork.Object, _mapper.Object);
    }

    #region GetAllAsync

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
        Expression<Func<CountryName, bool>>? capturedFilter = null;
        _countryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(), cancellation.Token))
            .Callback<Expression<Func<CountryName, bool>>, CancellationToken>((filter, _) => capturedFilter = filter)
            .ReturnsAsync(countries);

        var result = await _service.GetAllAsync(languageId, cancellation.Token);

        Assert.Same(countries, result);
        Assert.NotNull(capturedFilter);
        Assert.True(capturedFilter.Compile()(new CountryName { LanguageId = languageId }));
        Assert.False(capturedFilter.Compile()(new CountryName { LanguageId = "fr" }));
        _countryNameRepository.Verify(repository => repository.GetAllAsync<CountryResponse>(
            It.IsAny<Expression<Func<CountryName, bool>>>(), cancellation.Token), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyListWhenRepositoryHasNoCountries()
    {
        var countries = new List<CountryResponse>();
        _countryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(countries);

        var result = await _service.GetAllAsync("en");

        Assert.Same(countries, result);
    }

    #endregion

    #region GetPhoneCodeOptionsAsync

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
        Expression<Func<CountryName, bool>>? capturedFilter = null;
        _countryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryPhoneCodeResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(), cancellation.Token))
            .Callback<Expression<Func<CountryName, bool>>, CancellationToken>((filter, _) => capturedFilter = filter)
            .ReturnsAsync(options);

        var result = await _service.GetPhoneCodeOptionsAsync(languageId, cancellation.Token);

        Assert.Same(options, result);
        Assert.NotNull(capturedFilter);
        Assert.True(capturedFilter.Compile()(new CountryName { LanguageId = languageId }));
        Assert.False(capturedFilter.Compile()(new CountryName { LanguageId = "fr" }));
        _countryNameRepository.Verify(repository => repository.GetAllAsync<CountryPhoneCodeResponse>(
            It.IsAny<Expression<Func<CountryName, bool>>>(), cancellation.Token), Times.Once);
    }

    [Fact]
    public async Task GetPhoneCodeOptionsAsync_ReturnsEmptyListWhenRepositoryHasNoOptions()
    {
        var options = new List<CountryPhoneCodeResponse>();
        _countryNameRepository
            .Setup(repository => repository.GetAllAsync<CountryPhoneCodeResponse>(
                It.IsAny<Expression<Func<CountryName, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(options);

        var result = await _service.GetPhoneCodeOptionsAsync("en");

        Assert.Same(options, result);
    }

    #endregion

    #region GetByIdAsync

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
}
