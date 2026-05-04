using AutoMapper;
using CruxSuits.Infrastructure.EntityFramework.Entities.Languages;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared.Permissions.UiResources;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace CruxSuits.BackAPI.Services.Permissions.UiResources
{
    public class UiResourceService : IUiResourceService
    {
        private readonly ICruxSuitsUnitOfWork _unitOfWork;
        private readonly ILogger<UiResourceService> _logger;
        private readonly IMapper _mapper;

        public UiResourceService(ICruxSuitsUnitOfWork unitOfWork, ILogger<UiResourceService> logger, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UiResourcePermissionResponse>> GetSubUiRecourceByUserAsync(User user, string uiKey)
        {

            List<UiResourcePermissionResponse> permissionsByUser = await _unitOfWork
                .UserUiResourcePermission
                .GetAllAsync<UiResourcePermissionResponse>(c => c.UserId == user.Id && c.UiResource!.Key.StartsWith($"{uiKey}."));

            IEnumerable<int> specificResourceIds = permissionsByUser.Select(x => x.UiResourceId);

            List<UiResourcePermissionResponse> permissionsByRole = await _unitOfWork
                .RoleUiResourcePermission
                .GetAllAsync<UiResourcePermissionResponse>(c => c.RoleId == user.RoleId && c.UiResource!.Key.StartsWith($"{uiKey}.") && !specificResourceIds.Contains(c.UiResourceId));

            return permissionsByRole.Union(permissionsByUser);
        }

        public async Task<UiResourceResponse> CreateNewUiResourceAsync(UiResourceRequest request, string currentLanguage)
        {
            UiResource? parentUI = null;
            if (!string.IsNullOrEmpty(request.ParentUiResourceKey))
                parentUI = await _unitOfWork.UiResource.GetByKeyAsync(request.ParentUiResourceKey);

            UiResource entity = new()
            {
                Key = $"{parentUI?.Key}.{request.Key}".Trim('.').Trim(),
                UiResourceTypeId = request.UiResourceTypeId,
            };

            IEnumerable<UiResourceName> names = request.Names.Select(s => new UiResourceName
            {
                LanguageId = s.Key,
                Resource = entity,
                Name = s.Value,
            });

            await _unitOfWork.UiResource.AddAsync(entity);
            await _unitOfWork.UiResourceName.AddRangeAsync(names);
            await _unitOfWork.SaveChangesAsync();

            entity.Names = names.Where(c => c.LanguageId == currentLanguage).ToList();

            return _mapper.Map<UiResourceResponse>(entity);
        }
    }
}
