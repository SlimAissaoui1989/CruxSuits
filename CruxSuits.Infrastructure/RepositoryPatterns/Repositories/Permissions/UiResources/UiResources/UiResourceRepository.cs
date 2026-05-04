using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResources
{
    public class UiResourceRepository : CruxSuitsRepository<int, UiResource>, IUiResourceRepository
    {
        public UiResourceRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<UiResource?> GetByKeyOrDefaultAsync(string key)
        {
            return await GetFirstOrDefaultAsync(c => c.Key == key);
        }

        public async Task<UiResource> GetByKeyAsync(string key)
        {
            return await GetByKeyOrDefaultAsync(key) ?? throw EntityNotFoundException();
        }
    }
}
