using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResources
{
    public interface IUiResourceRepository : ICruxSuitsRepository<int, UiResource>
    {

        Task<UiResource?> GetByKeyOrDefaultAsync(string key);

        Task<UiResource> GetByKeyAsync(string key);
    }
}
