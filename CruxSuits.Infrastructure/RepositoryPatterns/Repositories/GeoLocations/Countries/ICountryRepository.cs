using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Countries
{
    public interface ICountryRepository : ICruxSuitsRepository<string, Country>
    {
        Task<string> GetCountryPhoneCode(string countryCode);
    }
}
