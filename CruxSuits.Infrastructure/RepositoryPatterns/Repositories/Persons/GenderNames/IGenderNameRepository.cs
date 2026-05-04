using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.GenderNames
{
    public interface IGenderNameRepository : ICruxSuitsRepository<string, GenderName>
    {
    }
}
