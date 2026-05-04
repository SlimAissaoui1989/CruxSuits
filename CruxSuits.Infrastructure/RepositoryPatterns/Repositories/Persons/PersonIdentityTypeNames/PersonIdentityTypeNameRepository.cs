using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentityTypeNames
{
    public class PersonIdentityTypeNameRepository : CruxSuitsRepository<string, PersonIdentityTypeName>, IPersonIdentityTypeNameRepository
    {
        public PersonIdentityTypeNameRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
