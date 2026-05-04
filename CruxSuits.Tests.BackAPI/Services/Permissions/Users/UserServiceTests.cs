using CruxSuits.BackAPI.Services.Permissions.Users;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Shared.Permissions.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.BackAPI.Services.Permissions.Users
{
    public class UserServiceTests
    {
        private readonly Mock<ICruxSuitsUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _userService;
        private readonly string jwtKey;
        private readonly string issuer;

        public UserServiceTests()
        {
            _mockUnitOfWork = new Mock<ICruxSuitsUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _userService = new UserService(_mockUnitOfWork.Object, _mockPasswordHasher.Object, _mockLogger.Object);
            jwtKey = "CruxSu1t$3rp@ppl1c4tionB4ck@piC0pyR1ghtCrux1t2026";
            issuer = "back.api.cruxsuits.tn";
        }

        [Fact]
        public async Task LoginAsync_ValidEmail_ReturnsUser()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "test@example.com",
                Password = "correctpassword"
            };

            var mockUser = new User
            {
                Id = 1,
                UserName = "testuser",
                Password = "hashedpassword", // This should be a hashed version of the password
                RoleId = 1,
                Role = new Role { Name = "User" }
            };

            _mockUnitOfWork.Setup(u => u.User.GetByEmailAsync(loginRequest.LoginId))
                .ReturnsAsync(mockUser);

            _mockPasswordHasher.Setup(p => p.VerifyHashedPassword(mockUser, mockUser.Password, loginRequest.Password))
                .Returns(PasswordVerificationResult.Success);

            // Act
            var user = await _userService.LoginAsync(loginRequest
                , IPAddress.Parse("127.0.0.1"));

            user.Should().NotBeNull();
            user.UserName.Should().Be(mockUser.UserName);
        }

        [Fact]
        public async Task LoginAsync_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "test@example.com",
                Password = "wrongpassword"
            };

            var mockUser = new User
            {
                Id = 1,
                UserName = "testuser",
                Password = "hashedpassword", // This should be a hashed version of the password
                RoleId = 1,
                Role = new Role { Name = "User" }
            };

            _mockUnitOfWork.Setup(u => u.User.GetByEmailAsync(loginRequest.LoginId))
                .ReturnsAsync(mockUser);

            _mockPasswordHasher.Setup(p => p.VerifyHashedPassword(mockUser, mockUser.Password, loginRequest.Password))
                .Returns(PasswordVerificationResult.Failed);

            // Act
            User? user = await _userService.LoginAsync(loginRequest, IPAddress.Parse("127.0.0.1"));

            // Assert
            user.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_EmailNotFound_ReturnsNull()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                LoginId = "nonexistent@example.com",
                Password = "anyPassword"
            };

            _mockUnitOfWork.Setup(u => u.User.GetByEmailAsync(loginRequest.LoginId))
                .ReturnsAsync((User?)null);

            // Act
            User? user = await _userService.LoginAsync(loginRequest, IPAddress.Parse("127.0.0.1"));

            user.Should().BeNull();
        }

        [Fact]
        public void GetAccessToken_ShouldReturnToken()
        {
            User mockUser = new()
            {
                Id = 99,
                IsEnabled = true,
                Password = "password",
                Role = new() { Id = 99, Level = "A", Name = "test_role", },
                UserName = "mocked_user"
            };

            string accessToke = _userService.GetAccessToken(mockUser, jwtKey, issuer);
            accessToke.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetRefreshToken_Should_Create_RefreshToken_And_Return_Token()
        {
            User user = new()
            {
                Id = 99,
                IsEnabled = true,
                Password = "password",
                Role = new() { Id = 99, Level = "A", Name = "test_role", },
                UserName = "mocked_user"
            };

            string accessToken = $"access_token";
            Guid deviceId = Guid.NewGuid();

            RefreshToken? mockRefreshToken = null;

            _mockUnitOfWork.Setup(s => s.RefreshToken.AddAsync(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(rt => mockRefreshToken = rt);

            string result = await _userService.GetRefreshTokenAsync(user, accessToken, deviceId);

            result.Should().NotBeEmpty();
        }

        [Fact]
        public void AppendAuthTokenCookie_Should_Set_Expected_Cookie()
        {
            var refreshToken = "TEST_REFRESH_TOKEN";

            HttpContext context = new DefaultHttpContext();
            HttpResponse httpResponse = context.Response;

            _userService.AppendAuthTokenCookie(httpResponse, refreshToken);

            httpResponse.Headers.TryGetValue("Set-Cookie", out var setCookieValues).Should().BeTrue();
            var setCookieHeader = setCookieValues.ToString();
            setCookieHeader.Should().NotBeNullOrEmpty();
        }
    }
}
