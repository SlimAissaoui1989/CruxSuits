using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using CruxSuits.Infrastructure.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.GeoLocations.Countries
{
    public static class CountryData
    {
        private static Country TestCountry => new Country 
        { 
            Id = "TN", 
            Id3Digit = "TUN",
            PhoneCode = "216",
            CurrencyId = "TND",
        };

        public static async Task InitializeTestCountryAsync(CruxSuitsUnitOfWork uow)
        {
            Country test = TestCountry;

            await uow.Country.AddAsync(test);
            await uow.SaveChangesAsync();
        }
    }
}
