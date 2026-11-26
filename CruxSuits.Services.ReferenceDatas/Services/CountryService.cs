using AutoMapper;
using CruxIT.Library;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;

namespace CruxSuits.Services.ReferenceDatas.Services
{
    public sealed class CountryService : ICountryService
    {
        private readonly IReferenceDataUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IAdministrativeAreaService _administrativeAreaService;

        public CountryService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper, IAdministrativeAreaService administrativeAreaService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _administrativeAreaService = administrativeAreaService;
        }

        public async Task<List<CountryResponse>> GetAllAsync(string languageId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Country.GetAllLocalizedAsync<CountryResponse>(languageId, cancellationToken);
        }

        public async Task<List<CountryPhoneCodeResponse>> GetPhoneCodeOptionsAsync(string languageId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.Country.GetAllLocalizedAsync<CountryPhoneCodeResponse>(languageId, cancellationToken);
        }

        public async Task<CountryDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var country = await _unitOfWork.Country.GetByIdOrDefaultAsync<CountryDetailsResponse>(id, cancellationToken);
            if (country is null)
                throw new CxException(CxHttpStatusCode.NotFound);

            return country;
        }

        public async Task<CountryResponse> CreateAsync(CountryRequest request, CancellationToken cancellationToken = default)
        {
            var country = _mapper.Map<Country>(request);
            await ValidateCountryAsync(country, isUpdate: false, cancellationToken);
            var createdCountry = await _unitOfWork.Country.AddAsync(country, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CountryResponse>(createdCountry);
        }

        public async Task<CountryResponse> UpdateAsync(string id, UpdateCountryRequest request, CancellationToken cancellationToken = default)
        {
            var existingCountry = await _unitOfWork.Country.GetByIdOrDefaultAsync(id, cancellationToken);
            if (existingCountry is null)
                throw new CxException(CxHttpStatusCode.NotFound);

            _mapper.Map(request, existingCountry);
            await ValidateCountryAsync(existingCountry, isUpdate: true, cancellationToken);
            var updatedCountry = await _unitOfWork.Country.UpdateAsync(existingCountry, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CountryResponse>(updatedCountry);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            var country = await _unitOfWork.Country.GetByIdOrDefaultAsync(id, cancellationToken);
            if (country is null)
                throw new CxException(CxHttpStatusCode.NotFound);

            await _administrativeAreaService.RemoveByCountryIdAsync(country.Id, cancellationToken);
            await _unitOfWork.Country.RemoveAsync(country, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task ValidateCountryAsync(Country country, bool isUpdate, CancellationToken cancellationToken)
        {
            if (country.Id is not { Length: 2 } || !country.Id.All(char.IsAsciiLetter))
                throw new CxException(CxHttpStatusCode.BadRequest, "Country ID must contain exactly two ASCII letters.");

            if (country.Alpha3Code is not { Length: 3 } || !country.Alpha3Code.All(char.IsAsciiLetter))
                throw new CxException(CxHttpStatusCode.BadRequest, "Alpha3Code must contain exactly three ASCII letters.");

            if (country.NumericCode is not { Length: 3 } || !country.NumericCode.All(char.IsAsciiDigit))
                throw new CxException(CxHttpStatusCode.BadRequest, "NumericCode must contain exactly three ASCII digits.");

            if (string.IsNullOrWhiteSpace(country.CurrencyId))
                throw new CxException(CxHttpStatusCode.BadRequest, "CurrencyId is required.");

            if (!isUpdate && await _unitOfWork.Country.GetByIdOrDefaultAsync(country.Id, cancellationToken) is not null)
                throw new CxException(CxHttpStatusCode.Conflict, $"Country '{country.Id}' already exists.");

            if (await _unitOfWork.Currency.GetByIdOrDefaultAsync(country.CurrencyId, cancellationToken) is null)
                throw new CxException(CxHttpStatusCode.BadRequest, $"Currency '{country.CurrencyId}' does not exist.");

            var duplicateAlpha3Code = await _unitOfWork.Country.GetFirstOrDefaultAsync(
                existing => existing.Id != country.Id && existing.Alpha3Code == country.Alpha3Code,
                cancellationToken);
            if (duplicateAlpha3Code is not null)
                throw new CxException(CxHttpStatusCode.Conflict,
                    $"Alpha3Code '{country.Alpha3Code}' is already assigned to another country.");

            var duplicateNumericCode = await _unitOfWork.Country.GetFirstOrDefaultAsync(
                existing => existing.Id != country.Id && existing.NumericCode == country.NumericCode,
                cancellationToken);
            if (duplicateNumericCode is not null)
                throw new CxException(CxHttpStatusCode.Conflict,
                    $"NumericCode '{country.NumericCode}' is already assigned to another country.");
        }
    }
}
