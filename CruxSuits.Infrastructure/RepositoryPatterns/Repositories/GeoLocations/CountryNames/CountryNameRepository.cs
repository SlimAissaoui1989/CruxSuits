using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.CountryNames
{
    public class CountryNameRepository : CruxSuitsRepository<string, CountryName>, ICountryNameRepository
    {
        public CountryNameRepository(CruxSuitsUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
