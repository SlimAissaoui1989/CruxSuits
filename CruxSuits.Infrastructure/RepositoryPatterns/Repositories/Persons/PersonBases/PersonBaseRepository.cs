using CruxIT.Library.DataToolkit.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonBases
{
    public class PersonBaseRepository<TPerson> : CruxSuitsRepository<int, TPerson>, IPersonBaseRepository<TPerson>
        where TPerson : PersonBase
    {
        public PersonBaseRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
