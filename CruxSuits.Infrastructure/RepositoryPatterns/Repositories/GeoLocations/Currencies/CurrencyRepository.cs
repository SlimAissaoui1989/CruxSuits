using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Currencies
{
    public class CurrencyRepository : CruxSuitsRepository<string, Currency>, ICurrencyRepository
    {
        public CurrencyRepository(CruxSuitsUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
