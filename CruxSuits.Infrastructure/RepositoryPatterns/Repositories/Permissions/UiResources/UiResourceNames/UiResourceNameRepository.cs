using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourceNames
{
    public class UiResourceNameRepository : CruxSuitsRepository<string, UiResourceName>, IUiResourceNameRepository
    {
        public UiResourceNameRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
