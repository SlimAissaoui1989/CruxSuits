using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonBases
{
    public interface IPersonBaseRepository<TPerson> : ICruxSuitsRepository<int, TPerson>
        where TPerson : PersonBase
    {
    }
}
