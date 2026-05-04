using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentityTypes
{
    public interface IPersonIdentityTypeRepository : ICruxSuitsRepository<string, PersonIdentityType>
    {
    }
}
