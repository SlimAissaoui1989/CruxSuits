using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using CruxSuits.Infrastructure.EntityFramework.Entities.GeoLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.Phones
{
    public class PhoneRepository : CruxSuitsRepository<int, Phone>, IPhoneRepository
    {
        public PhoneRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
