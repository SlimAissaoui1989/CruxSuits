using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.BackAPI.Controllers.Permissions;
using CruxSuits.BackAPI.Services.Permissions.Users;
using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared;
using CruxSuits.Shared.Permissions.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CruxSuits.Tests.BackAPI.Controller.Permissions.Users
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<CxControllerBase<CruxSuitsContext, ICruxSuitsUnitOfWork>>> _mockLogger;
        private readonly Mock<ICruxSuitsUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsersController _controller;
        private readonly Mock<HttpContext> _mockHttpContext;
        //private readonly Mock<HttpRequest> _mockHttpRequest;

        public UsersControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<CxControllerBase<CruxSuitsContext, ICruxSuitsUnitOfWork>>>();
            _mockUnitOfWork = new Mock<ICruxSuitsUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContext = new Mock<HttpContext>();
            //_mockHttpRequest = new Mock<HttpRequest>();

            // Setup the mock for Jwt:Key and Jwt:Issuer configuration values
            _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("test-secret-key");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("test-issuer");

            _mockHttpContext.Setup(s => s.Request.Headers[It.IsAny<string>()])
                .Returns(StringValues.Empty);

            _mockHttpContext.Setup(s => s.Items)
                .Returns(new Dictionary<object, object?>());

            _mockHttpContext.Setup(s => s.Connection.RemoteIpAddress)
                .Returns((IPAddress?)null);

            // Create the controller
            _controller = new UsersController(
                _mockConfiguration.Object,
                _mockLogger.Object,
                _mockUnitOfWork.Object,
                _mockMapper.Object,
                _mockUserService.Object
            )
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object,
                }
            };
        }

        [Fact]
        public async Task LoginAsync_ValidLogin_ReturnsOk()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "test@example.com",
                Password = "correctpassword"
            };

            User user = new()
            {
                Id = 99,
                IsEnabled = true,
                Password = "password",
                Role = new() { Id = 99, Level = "A", Name = "test_role", },
                UserName = "mocked_user"
            };

            UserResponse loginResponse = new()
            {
                FirstName = "test",
                FullName = "test",
                Id = 99,
                LastName = "test",
                RoleId = 99,
                RoleName = "test_role",
                UserName = user.UserName
            };

            Guid deviceId = Guid.NewGuid();
            StringValues deviceIdStr = new StringValues(deviceId.ToString());
            StringValues connectionId = new StringValues("CONNECTION_ID");

            string accessToken = "ACCESS_TOKEN";
            string refreshToken = "Refresh_TOKEN";

            _mockUserService.Setup(u => u.LoginAsync(loginRequest, null))
                .ReturnsAsync(user);

            _mockUserService.Setup(u => u.GetAccessToken(user, It.IsAny<string>(), It.IsAny<string>()))
                .Returns(accessToken);

            _mockUserService.Setup(u => u.GetRefreshTokenAsync(user, accessToken, deviceId))
                .ReturnsAsync(refreshToken);

            _mockUserService.Setup(u => u.AppendAuthTokenCookie(_mockHttpContext.Object.Response, refreshToken));

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceIdStr))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(true);

            _mockMapper.Setup(s => s.Map<UserResponse>(user))
                .Returns(loginResponse);

            // Act
            var result = await _controller.LoginAsync(loginRequest);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            actionResult.Value.Should().BeOfType<LoginResponse>();
            LoginResponse loginResponseResult = (LoginResponse)actionResult.Value;
            loginResponseResult.Token.Should().Be(accessToken);
            loginResponseResult.User.Should().Be(loginResponse);
        }

        [Fact]
        public async Task LoginAsync_InvalidLogin_ReturnsUnauthorized()
        {

            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "test@example.com",
                Password = "correctpassword"
            };

            Guid deviceId = Guid.NewGuid();
            StringValues deviceIdStr = new StringValues(deviceId.ToString());
            StringValues connectionId = new StringValues("CONNECTION_ID");

            _mockUserService.Setup(u => u.LoginAsync(loginRequest, null))
                .ReturnsAsync((User?)null);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceIdStr))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(true);

            // Act
            var result = await _controller.LoginAsync(loginRequest);

            // Assert
            var actionResult = Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task LoginAsync_DeviceIdNotFound_ReturnsBadRequest()
        {

            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "test@example.com",
                Password = "correctpassword"
            };

            StringValues deviceIdStr = StringValues.Empty;

            StringValues connectionId = new StringValues("CONNECTION_ID");

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceIdStr))
                .Returns(false);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(true);

            // Act
            var result = await _controller.LoginAsync(loginRequest);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task LoginAsync_ConnectionIdNotFound_ReturnsBadRequest()
        {

            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "test@example.com",
                Password = "correctpassword"
            };

            StringValues deviceIdStr = new StringValues(Guid.NewGuid().ToString());

            StringValues connectionId = StringValues.Empty;

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceIdStr))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(false);

            // Act
            var result = await _controller.LoginAsync(loginRequest);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ValidateToken_ReturnNoContent_WhenTokenIsValid()
        {
            _mockHttpContext.Setup(s => s.User.Identity!.IsAuthenticated)
                .Returns(true);
            var result = _controller.ValidateToken();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void ValidateToken_ReturnUnauthorized_WhenTokenIsInvalid()
        {
            _mockHttpContext.Setup(s => s.User.Identity!.IsAuthenticated)
                .Returns(false);
            var result = _controller.ValidateToken();
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task LogoutAsync_ReturnNoContent_Always()
        {
            var result = await _controller.LogoutAsync();
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
