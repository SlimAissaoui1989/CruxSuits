using AutoMapper;
using CruxIT.Library;
using CruxIT.Library.Exceptions;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Shared.DTOs.ReferenceDatas;

namespace CruxSuits.Services.ReferenceDatas.Services
{
    public sealed class CountryService : ICountryService
    {
        private readonly IReferenceDataUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IReferenceDataUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CountryResponse>> GetAllAsync(string languageId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CountryName.GetAllAsync<CountryResponse>(countryName => countryName.LanguageId == languageId, cancellationToken);
        }

        public async Task<List<CountryPhoneCodeResponse>> GetPhoneCodeOptionsAsync(string languageId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.CountryName.GetAllAsync<CountryPhoneCodeResponse>(countryName => countryName.LanguageId == languageId, cancellationToken);
        }

        public async Task<CountryDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var country = await _unitOfWork.Country.GetByIdOrDefaultAsync<CountryDetailsResponse>(id, cancellationToken);
            if (country is null)
                throw new CxException(CxExceptionTypes.NotFound);

            return country;
        }

        public async Task<CountryResponse> CreateAsync(CountryRequest request, CancellationToken cancellationToken = default)
        {
            var country = _mapper.Map<Country>(request);
            var createdCountry = await _unitOfWork.Country.AddAsync(country, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CountryResponse>(createdCountry);
        }

        public async Task<CountryResponse> UpdateAsync(string id, UpdateCountryRequest request, CancellationToken cancellationToken = default)
        {
            var existingCountry = await _unitOfWork.Country.GetByIdOrDefaultAsync(id, cancellationToken);
            if (existingCountry is null)
                throw new CxException(CxExceptionTypes.NotFound);

            _mapper.Map(request, existingCountry);
            var updatedCountry = await _unitOfWork.Country.UpdateAsync(existingCountry, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CountryResponse>(updatedCountry);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            var country = await _unitOfWork.Country.GetByIdOrDefaultAsync(id, cancellationToken);
            if (country is null)
                throw new CxException(CxExceptionTypes.NotFound);

            await _unitOfWork.Country.RemoveAsync(country, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
