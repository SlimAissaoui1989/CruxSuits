using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Languages.Languages
{
    public class LanguageRepository : CruxSuitsRepository<string, Language>, ILanguageRepository
    {
        public LanguageRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
