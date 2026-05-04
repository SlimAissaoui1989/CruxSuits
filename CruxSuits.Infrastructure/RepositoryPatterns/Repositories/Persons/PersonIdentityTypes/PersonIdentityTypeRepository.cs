using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentityTypes
{
    public class PersonIdentityTypeRepository : CruxSuitsRepository<string, PersonIdentityType>, IPersonIdentityTypeRepository
    {
        public PersonIdentityTypeRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
