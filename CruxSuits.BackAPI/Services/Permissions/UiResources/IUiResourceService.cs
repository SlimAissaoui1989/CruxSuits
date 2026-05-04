using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Shared.Permissions.UiResources;

namespace CruxSuits.BackAPI.Services.Permissions.UiResources
{
    public interface IUiResourceService
    {
        Task<IEnumerable<UiResourcePermissionResponse>> GetSubUiRecourceByUserAsync(User user, string pageKey);
        Task<UiResourceResponse> CreateNewUiResourceAsync(UiResourceRequest request, string currentLanguage);
    }
}
