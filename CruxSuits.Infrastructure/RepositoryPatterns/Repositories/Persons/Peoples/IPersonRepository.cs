using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.Peoples
{
    public interface IPersonRepository : IPersonBaseRepository<Person>
    {
    }
}
