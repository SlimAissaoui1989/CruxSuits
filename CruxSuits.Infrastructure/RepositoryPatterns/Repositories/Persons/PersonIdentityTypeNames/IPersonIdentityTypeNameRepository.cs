using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentityTypeNames
{
    public interface IPersonIdentityTypeNameRepository : ICruxSuitsRepository<string, PersonIdentityTypeName>
    {
    }
}
