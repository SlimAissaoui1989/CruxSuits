using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Adresses
{
    public class AddressRepository : CruxSuitsRepository<int, Address>, IAddressRepository
    {
        public AddressRepository(CruxSuitsUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
