using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class CountryRepository : ReferenceDataEntityRepository<string, Country>, ICountryRepository
    {
        public CountryRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override async Task<Country> AddAsync(Country entity, CancellationToken cancellationToken = default)
        {
            ValidateCountryCodes(entity);
            await ValidateCurrencyAsync(entity.CurrencyId, cancellationToken);

            if (await DbSet.AnyAsync(country => country.Id == entity.Id, cancellationToken))
                throw new InvalidOperationException($"Country '{entity.Id}' already exists.");

            await ValidateUniqueCodesAsync(entity, cancellationToken);

            return await base.AddAsync(entity, cancellationToken);
        }

        public override async Task<Country> UpdateAsync(Country entity, CancellationToken cancellationToken = default)
        {
            ValidateCountryCodes(entity);
            await ValidateCurrencyAsync(entity.CurrencyId, cancellationToken);
            await ValidateUniqueCodesAsync(entity, cancellationToken);

            return await base.UpdateAsync(entity, cancellationToken);
        }

        private static void ValidateCountryCodes(Country country)
        {
            if (country.Id is not { Length: 2 } || !country.Id.All(char.IsAsciiLetter))
                throw new ArgumentException("Country ID must contain exactly two ASCII letters.", nameof(country));

            if (country.Alpha3Code is not { Length: 3 } || !country.Alpha3Code.All(char.IsAsciiLetter))
                throw new ArgumentException("Alpha3Code must contain exactly three ASCII letters.", nameof(country));

            if (country.NumericCode is not { Length: 3 } || !country.NumericCode.All(char.IsAsciiDigit))
                throw new ArgumentException("NumericCode must contain exactly three ASCII digits.", nameof(country));

            if (string.IsNullOrWhiteSpace(country.CurrencyId))
                throw new ArgumentException("CurrencyId is required.", nameof(country));
        }

        private async Task ValidateCurrencyAsync(string currencyId, CancellationToken cancellationToken)
        {
            if (await UnitOfWork.Currency.GetByIdOrDefaultAsync(currencyId, cancellationToken) is null)
                throw new ArgumentException($"Currency '{currencyId}' does not exist.", nameof(currencyId));
        }

        private async Task ValidateUniqueCodesAsync(Country country, CancellationToken cancellationToken)
        {
            if (await DbSet.AnyAsync(existing => existing.Id != country.Id && existing.Alpha3Code == country.Alpha3Code, cancellationToken))
                throw new InvalidOperationException($"Alpha3Code '{country.Alpha3Code}' is already assigned to another country.");

            if (await DbSet.AnyAsync(existing => existing.Id != country.Id && existing.NumericCode == country.NumericCode, cancellationToken))
                throw new InvalidOperationException($"NumericCode '{country.NumericCode}' is already assigned to another country.");
        }
    }
}
