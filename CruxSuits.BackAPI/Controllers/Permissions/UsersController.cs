using AutoMapper;
using CruxIT.Library.API;
using CruxIT.Library.API.Controller;
using CruxSuits.BackAPI.CustomControllerBase;
using CruxSuits.BackAPI.Models;
using CruxSuits.BackAPI.Services.Permissions.Users;
using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared;
using CruxSuits.Shared.Permissions.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CruxSuits.BackAPI.Controllers.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CxSuitsController
    {
        private readonly IUserService _userService;

        public UsersController(IConfiguration configuration,
            ILogger<CxControllerBase<CruxSuitsContext, ICruxSuitsUnitOfWork>> logger,
            ICruxSuitsUnitOfWork unitOfWork,
            IMapper mapper,
            IUserService userService)
            : base(configuration, logger, unitOfWork, mapper)
        {
            this._userService = userService;
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult<UserResponse>> LoginAsync(LoginRequest request)
        {

            if (!Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out var deviceIdStr)
                || string.IsNullOrEmpty(deviceIdStr)
                || !Guid.TryParse(deviceIdStr, out Guid deviceId))
                return BadRequest();

            if (!Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out var connectionId)
                || string.IsNullOrEmpty(connectionId))
                return BadRequest();

            User? user = await _userService.LoginAsync(request, IpAdress);

            if (user == null) return Unauthorized();

            string accessToken = _userService.GetAccessToken(user, Config["Jwt:Key"]!, Config["Jwt:Issuer"]!);
            string refreshToken = await _userService.GetRefreshTokenAsync(user, accessToken, deviceId);

            _userService.AppendAuthTokenCookie(Response, refreshToken);

            _userService.AddNewConnection(refreshToken, user.Id, connectionId!, deviceId);

            LoginResponse response = new()
            {
                Token = accessToken,
                User = Mapper.Map<UserResponse>(user),
            };

            return Ok(response);
        }

        [HttpPost("auth/logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            _userService.DeleteAuthTokenCookie(Response);
            await _userService.RevokeRefreshTokenAsync(Request);

            return NoContent();
        }

        [HttpPost("auth/refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            string newAccessToken = await _userService.TryToRefreshTokenAsync(Request, Response, Config["Jwt:Key"]!, Config["Jwt:Issuer"]!);
            return Ok(newAccessToken);
        }

        [HttpGet("auth/validate-token")]
        public IActionResult ValidateToken()
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? false)
                return NoContent();
            return Unauthorized();
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserResponse>> GetMe()
        {
            return await UnitOfWork.User.GetByIdAsync<UserResponse>(CurrentUserId!.Value);
        }
    }
}
