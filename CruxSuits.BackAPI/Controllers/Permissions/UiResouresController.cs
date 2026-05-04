using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.BackAPI.CustomControllerBase;
using CruxSuits.BackAPI.Services.Permissions.UiResources;
using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared.Permissions.UiResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.BackAPI.Controllers.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class UiResouresController : CxSuitsController
    {
        private IUiResourceService _uiResourceService;
        public UiResouresController(IConfiguration configuration,
            ILogger<CxControllerBase<CruxSuitsContext, ICruxSuitsUnitOfWork>> logger,
            ICruxSuitsUnitOfWork unitOfWork,
            IMapper mapper,
            IUiResourceService uiResourceService) : base(configuration, logger, unitOfWork, mapper)
        {
            _uiResourceService = uiResourceService;
        }

        [HttpGet("subUiResource/{uiResourceKey}")]
        [Authorize]
        public async Task<ActionResult<UiResourcePermissionResponse>> GetSubUiRecources(string uiResourceKey)
        {
            User? user = await UnitOfWork.User.GetByUserNameAsync(CurrentUserName!);
            if (user == null)
                return Unauthorized();

            UiResource? uiResource = await UnitOfWork.UiResource.GetByKeyOrDefaultAsync(uiResourceKey);
            if (uiResource == null)
                return NotFound();

            IEnumerable<UiResourcePermissionResponse> result = await _uiResourceService.GetSubUiRecourceByUserAsync(user, uiResourceKey);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<UiResourceResponse>> CreateUiResource(UiResourceRequest request)
        {
            if (CurrentRoleId != 1)
                return Unauthorized();

            UiResourceResponse response = await _uiResourceService.CreateNewUiResourceAsync(request, CurrentLanguageId!);
            return Ok(response);
        }
    }
}
