using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.CountryNames
{
    public interface ICountryNameRepository : ICruxSuitsRepository<string, CountryName>
    {
    }
}
