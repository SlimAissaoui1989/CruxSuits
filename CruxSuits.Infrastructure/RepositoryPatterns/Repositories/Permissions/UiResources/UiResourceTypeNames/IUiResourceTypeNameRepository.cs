using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourceTypeNames
{
    public interface IUiResourceTypeNameRepository : ICruxSuitsRepository<string, UiResourceTypeName>
    {
    }
}
