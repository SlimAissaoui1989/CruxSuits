using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.CurrencyNames
{
    public class CurrencyNameRepository : CruxSuitsRepository<string, CurrencyName>, ICurrencyNameRepository
    {
        public CurrencyNameRepository(CruxSuitsUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
