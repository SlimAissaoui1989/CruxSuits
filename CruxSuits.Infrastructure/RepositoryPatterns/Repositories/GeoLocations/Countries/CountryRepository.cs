using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Countries
{
    public class CountryRepository : CruxSuitsRepository<string, Country>, ICountryRepository
    {
        public CountryRepository(CruxSuitsUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<string> GetCountryPhoneCode(string countryCode)
        {
            Country? country = null;

            switch (countryCode.Length)
            {
                case 2:
                    country = await GetByIdAsync(countryCode);
                    break;
                case 3:
                    country = await GetFirstAsync(c => c.Id3Digit == countryCode);
                    break;
                default:
                    throw EntityNotFoundException();
            };

            return $"+{country.PhoneCode}";
        }
    }
}
