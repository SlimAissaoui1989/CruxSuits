using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern;
using CruxSuits.Services.Identity.Services;
using CruxSuits.Shared;
using CruxSuits.Shared.DTOs.Identities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.Services.Identity.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : CxControllerBase<IdentityContext, IIdentityUnitOfWork>
    {
        private readonly IAuthService _authService;
        public AuthController(IConfiguration configuration,
            ILogger<CxControllerBase<IdentityContext, IIdentityUnitOfWork>> logger,
            IIdentityUnitOfWork unitOfWork,
            IMapper mapper,
            IAuthService authService) : base(configuration, logger, unitOfWork, mapper)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!Request.Headers.TryGetValue(SharedValues.DeviceIdHeaderTag, out var deviceIdStr)
                || string.IsNullOrEmpty(deviceIdStr)
                || !Guid.TryParse(deviceIdStr, out Guid deviceId))
                return BadRequest();

            LoginResponse? response = await _authService.LoginAsync(request, IpAdress, deviceId);

            return Ok(response);
        }
    }
}
