using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourceTypes
{
    public class UiResourceTypeRepository : CruxSuitsRepository<string, UiResourceType>, IUiResourceTypeRepository
    {
        public UiResourceTypeRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
