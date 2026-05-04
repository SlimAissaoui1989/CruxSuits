using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Infrastructure.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.UiResources
{
    public class UiResourceData
    {
        private static UiResource UiResourceTest => new UiResource()
        {
            Id = 99,
            Key = "UiResource",
            UiResourceType = new() { Id = "CxTest", HasDeletePermission = true, HasEditPermission = true },
        };

        public static async Task InitializeUiResourceAsync(CruxSuitsUnitOfWork uow)
        {
            UiResource resource = UiResourceTest;

            await uow.UiResource.AddAsync(resource);
            await uow.SaveChangesAsync();
        }


    }
}
