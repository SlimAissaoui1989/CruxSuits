using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Languages.Languages
{
    public interface ILanguageRepository : ICruxSuitsRepository<string, Language>
    {
    }
}
