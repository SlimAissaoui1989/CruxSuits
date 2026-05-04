using Azure;
using Azure.Core;
using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxIT.Library.Exceptions;
using CruxSuits.BackAPI;
using CruxSuits.BackAPI.Models;
using CruxSuits.BackAPI.Services.Permissions.Users;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.RefreshTokens;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Users;
using CruxSuits.Shared;
using CruxSuits.Tests.Infrastructure.Tests;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.BackAPI.Services.Permissions.Users
{
    public class UserService_TryToRefreshTokenAsync_Tests
    {
        private readonly Mock<ICruxSuitsUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly TestableUserService _userService;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly string jwtKey;
        private readonly string issuer;

        public UserService_TryToRefreshTokenAsync_Tests()
        {
            _mockUnitOfWork = new Mock<ICruxSuitsUnitOfWork>();
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockLogger = new Mock<ILogger<UserService>>();
            _userService = new TestableUserService(_mockUnitOfWork.Object, _mockPasswordHasher.Object, _mockLogger.Object);
            _mockHttpContext = new Mock<HttpContext>();
            jwtKey = "CruxSu1t$3rp@ppl1c4tionB4ck@piC0pyR1ghtCrux1t2026";
            issuer = "back.api.cruxsuits.tn";
        }

        [Fact]
        public void ExtractRequestDataOrThrow_HappyPath_ReturnsValues()
        {
            var connectionId = new Microsoft.Extensions.Primitives.StringValues("conn-123");
            var deviceId = new Microsoft.Extensions.Primitives.StringValues(Guid.NewGuid().ToString());
            var accessToken = new Microsoft.Extensions.Primitives.StringValues("ACCESS_TOKEN");
            var refreshToken = "REFRESH_TOKEN";

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceId))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.BearerTag, out accessToken))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Cookies.TryGetValue(ConstsValues.AuthTokenCookieTag, out refreshToken))
                .Returns(true);

            // Act
            var result = _userService.CallExtractRequestDataOrThrow(_mockHttpContext.Object.Request);

            result.connectionId.Should().Be(connectionId);
            result.deviceId.Should().Be(deviceId);
            result.accessToken.Should().Be(accessToken);
            result.oldToken.Should().Be(refreshToken);
        }

        [Fact]
        public void ExtractRequestDataOrThrow_MissingOneValue_Throws()
        {
            var connectionId = new Microsoft.Extensions.Primitives.StringValues("conn-123");
            var deviceId = new Microsoft.Extensions.Primitives.StringValues(Guid.NewGuid().ToString());
            var accessToken = new Microsoft.Extensions.Primitives.StringValues("ACCESS_TOKEN");
            var refreshToken = "REFRESH_TOKEN";

            for (int i = 0; i < 4; i++)
            {
                _mockHttpContext.Reset();

                _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                    .Returns(i != 0);

                _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceId))
                    .Returns(i != 1);

                _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.BearerTag, out accessToken))
                    .Returns(i != 2);

                _mockHttpContext.Setup(s => s.Request.Cookies.TryGetValue(ConstsValues.AuthTokenCookieTag, out refreshToken))
                    .Returns(i != 3);

                CxException ex = Assert.Throws<CxException>(() => _userService.CallExtractRequestDataOrThrow(_mockHttpContext.Object.Request));

                ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
            }
        }

        [Fact]
        public async Task GetValidExistingRefreshTokenOrThrowAsync_ValidToken_ReturnsToken()
        {
            var oldToken = "OLD_REFRESH";
            var accessToken = "ACCESS";

            var existing = new RefreshToken
            {
                RefreshTokenValue = oldToken,
                AcessToken = accessToken,
                IsRevoked = false,
                Expires = DateTime.UtcNow.AddMinutes(10)
            };

            _mockUnitOfWork.Setup(r => r.RefreshToken.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync(existing);

            var result = await _userService.CallGetValidExistingRefreshTokenOrThrowAsync(oldToken, accessToken);

            result.Should().NotBeNull();
            result.Should().BeSameAs(existing);
        }

        [Fact]
        public async Task GetValidExistingRefreshTokenOrThrowAsync_NullToken_Throws()
        {
            _mockUnitOfWork.Setup(r => r.RefreshToken.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync((RefreshToken?)null);

            var ex = await Assert.ThrowsAsync<CxException>(
                () => _userService.CallGetValidExistingRefreshTokenOrThrowAsync("OLD", "ACCESS"));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public async Task GetValidExistingRefreshTokenOrThrowAsync_Revoked_Throws()
        {
            var existing = new RefreshToken
            {
                IsRevoked = true,
                Expires = DateTime.UtcNow.AddMinutes(10)
            };

            _mockUnitOfWork.Setup(r => r.RefreshToken.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync(existing);

            var ex = await Assert.ThrowsAsync<CxException>(
                () => _userService.CallGetValidExistingRefreshTokenOrThrowAsync("OLD", "ACCESS"));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public async Task GetValidExistingRefreshTokenOrThrowAsync_Expired_Throws()
        {
            var existing = new RefreshToken
            {
                IsRevoked = false,
                Expires = DateTime.UtcNow.AddMinutes(-1)
            };

            _mockUnitOfWork.Setup(r => r.RefreshToken.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync(existing);

            var ex = await Assert.ThrowsAsync<CxException>(
                () => _userService.CallGetValidExistingRefreshTokenOrThrowAsync("OLD", "ACCESS"));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public void ValidateConnectionOrThrow_HappyPath_ReturnsConnection()
        {
            var oldToken = "REFRESH";
            var connectionId = "conn-1";
            var deviceId = Guid.NewGuid();
            int userId = 99;

            var existingToken = new RefreshToken
            {
                DeviceId = deviceId
            };

            VolatileDataStore.RefreshTokenConnections.Clear();
            VolatileDataStore.RefreshTokenConnections.Add(new RefreshTokenConnections
            {
                RefreshToken = oldToken,
                Connections = new List<HubConnectionInfo>
                {
                    new HubConnectionInfo{ ConnectionId = connectionId, DeviceId = deviceId, UserId = userId },
                }
            });

            var result = _userService.CallValidateConnectionOrThrow(oldToken, connectionId, existingToken);

            result.RefreshToken.Should().Be(oldToken);
        }

        [Fact]
        public void ValidateConnectionOrThrow_TokenNotFound_Throws()
        {

            var ex = Assert.Throws<CxException>(
                () => _userService.CallValidateConnectionOrThrow("REFRESH", "conn-1", new RefreshToken()));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public void ValidateConnectionOrThrow_ConnectionNotFound_Throws()
        {
            var oldToken = "REFRESH";


            VolatileDataStore.RefreshTokenConnections.Clear();
            VolatileDataStore.RefreshTokenConnections.Add(
                new RefreshTokenConnections
                {
                    RefreshToken = oldToken,
                    Connections = new List<HubConnectionInfo>() // empty
                });

            var ex = Assert.Throws<CxException>(
                () => _userService.CallValidateConnectionOrThrow(oldToken, "conn-1", new RefreshToken()));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }


        [Fact]
        public void ValidateConnectionOrThrow_DeviceMismatch_Throws()
        {
            var oldToken = "REFRESH";
            var existingToken = new RefreshToken
            {
                DeviceId = Guid.NewGuid()
            };

            VolatileDataStore.RefreshTokenConnections.Clear();
            VolatileDataStore.RefreshTokenConnections.Add(
                new RefreshTokenConnections
                {
                    RefreshToken = oldToken,
                    Connections = new List<HubConnectionInfo>() // empty
                });

            var ex = Assert.Throws<CxException>(
                () => _userService.CallValidateConnectionOrThrow(oldToken, "conn-1", existingToken));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public async Task GetValidUserOrThrowAsync_HappyPath_ReturnsUser()
        {
            var existingToken = new RefreshToken
            {
                UserName = "user",
                Password = "pwd"
            };

            var user = new User
            {
                UserName = "user",
                Password = "pwd"
            };
            _mockUnitOfWork.Setup(r => r.User.GetByUserNameAsync(existingToken.UserName))
                .ReturnsAsync(user);

            var result = await _userService.CallGetValidUserOrThrowAsync(existingToken);

            result.Should().BeSameAs(user);
        }

        [Fact]
        public async Task GetValidUserOrThrowAsync_UserNotFound_Throws()
        {
            var existingToken = new RefreshToken
            {
                UserName = "user",
                Password = "pwd"
            };

            _mockUnitOfWork.Setup(r => r.User.GetByUserNameAsync(existingToken.UserName))
                .ReturnsAsync((User?)null);

            var ex = await Assert.ThrowsAsync<CxException>(
                () => _userService.CallGetValidUserOrThrowAsync(existingToken));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public async Task GetValidUserOrThrowAsync_InvalidPassword_Throws()
        {
            var existingToken = new RefreshToken
            {
                UserName = "user",
                Password = "pwd"
            };

            var user = new User
            {
                UserName = "user",
                Password = "other"
            };

            _mockUnitOfWork.Setup(r => r.User.GetByUserNameAsync(existingToken.UserName))
                .ReturnsAsync(user);

            var ex = await Assert.ThrowsAsync<CxException>(
                () => _userService.CallGetValidUserOrThrowAsync(existingToken));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }

        [Fact]
        public async Task GetRefreshTokenAsync_CreatesAndPersistsToken()
        {
            var user = new User
            {
                Id = 1,
                UserName = "user",
                Password = "pwd"
            };
            var deviceId = Guid.NewGuid();
            var accessToken = "ACCESS";

            RefreshToken? saved = null;

            _mockUnitOfWork
                .Setup(r => r.RefreshToken.AddAsync(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(rt => saved = rt);

            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var before = DateTime.UtcNow;

            var returnedToken = await _userService.GetRefreshTokenAsync(user, accessToken, deviceId);

            var after = DateTime.UtcNow;

            returnedToken.Should().NotBeNullOrEmpty();
            saved.Should().NotBeNull();

            Assert.Equal(user.Id, saved!.UserId);
            Assert.Equal(user.UserName, saved.UserName);
            Assert.Equal(user.Password, saved.Password);
            Assert.Equal(deviceId, saved.DeviceId);
            Assert.False(saved.IsRevoked);

            Assert.Equal(returnedToken, saved.AcessToken);
            Assert.Equal(returnedToken, saved.RefreshTokenValue);

            var expectedMin = before.Add(ConstsValues.RefreshTokenExpiredTime).AddSeconds(-5);
            var expectedMax = after.Add(ConstsValues.RefreshTokenExpiredTime).AddSeconds(5);
            Assert.InRange(saved.Expires, expectedMin, expectedMax);

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task TryToRefreshTokenAsync_HappyPath_Works()
        {
            var connectionId = new Microsoft.Extensions.Primitives.StringValues("conn-123");
            var deviceId = Guid.NewGuid();
            var deviceIdStr = new StringValues(deviceId.ToString());
            var accessToken = new StringValues("OLD_ACCESS");
            var refreshToken = "OLD_REFRESH";

            User user = new()
            {
                Id = 99,
                IsEnabled = true,
                Password = "password",
                Role = new() { Id = 99, Level = "A", Name = "test_role", },
                UserName = "mocked_user"
            };

            var existingToken = new RefreshToken
            {
                UserId = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                DeviceId = deviceId,
                AcessToken = accessToken.ToString(),
                RefreshTokenValue = refreshToken,
                Expires = DateTime.UtcNow.AddMinutes(10),
                IsRevoked = false
            };

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceIdStr))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.BearerTag, out accessToken))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Cookies.TryGetValue(ConstsValues.AuthTokenCookieTag, out refreshToken))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Response.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));

            VolatileDataStore.RefreshTokenConnections.Clear();
            VolatileDataStore.RefreshTokenConnections.Add(
            new RefreshTokenConnections
            {
                RefreshToken = refreshToken,
                Connections = new List<HubConnectionInfo>
                {
                    new HubConnectionInfo
                    {
                        ConnectionId = connectionId.ToString(),
                        DeviceId = deviceId
                    }
                }
            });

            RefreshToken createdRefreshToken = null!;

            _mockUnitOfWork.Setup(r => r.RefreshToken.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync(existingToken);

            _mockUnitOfWork.Setup(r => r.RefreshToken.AddAsync(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(rt => createdRefreshToken = rt);

            _mockUnitOfWork.Setup(r => r.User.GetByUserNameAsync(existingToken.UserName))
                .ReturnsAsync(user);


            var newAccessToken = await _userService.TryToRefreshTokenAsync(
                _mockHttpContext.Object.Request,
                _mockHttpContext.Object.Response,
                jwtKey,
                issuer);

            Assert.False(string.IsNullOrWhiteSpace(newAccessToken));

            Assert.True(existingToken.IsRevoked);
            Assert.NotNull(createdRefreshToken);

            lock (VolatileDataStore.RefreshTokenConnectionsLock)
            {
                var conn = VolatileDataStore.RefreshTokenConnections
                    .First(x => x.Connections.Any(c => c.ConnectionId == connectionId));

                Assert.Equal(createdRefreshToken!.RefreshTokenValue, conn.RefreshToken);
            }

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task TryToRefreshTokenAsync_ConcurrencyError_Throws()
        {
            var connectionId = new Microsoft.Extensions.Primitives.StringValues("conn-123");
            var deviceId = Guid.NewGuid();
            var deviceIdStr = new StringValues(deviceId.ToString());
            var accessToken = new StringValues("OLD_ACCESS");
            var refreshToken = "OLD_REFRESH";

            User user = new()
            {
                Id = 99,
                IsEnabled = true,
                Password = "password",
                Role = new() { Id = 99, Level = "A", Name = "test_role", },
                UserName = "mocked_user"
            };

            var existingToken = new RefreshToken
            {
                UserId = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                DeviceId = deviceId,
                AcessToken = accessToken.ToString(),
                RefreshTokenValue = refreshToken,
                Expires = DateTime.UtcNow.AddMinutes(10),
                IsRevoked = false
            };

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.ConnectionIdHeaderTag, out connectionId))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.DeviceIdHeaderTag, out deviceIdStr))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Headers.TryGetValue(SharedConstValues.BearerTag, out accessToken))
                .Returns(true);

            _mockHttpContext.Setup(s => s.Request.Cookies.TryGetValue(ConstsValues.AuthTokenCookieTag, out refreshToken))
                .Returns(true);

            VolatileDataStore.RefreshTokenConnections.Clear();
            VolatileDataStore.RefreshTokenConnections.Add(
            new RefreshTokenConnections
            {
                RefreshToken = refreshToken,
                Connections = new List<HubConnectionInfo>
                {
                    new HubConnectionInfo
                    {
                        ConnectionId = connectionId.ToString(),
                        DeviceId = deviceId
                    }
                }
            });

            RefreshToken createdRefreshToken = null!;

            _mockUnitOfWork.Setup(r => r.RefreshToken.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync(existingToken);

            _mockUnitOfWork.Setup(r => r.RefreshToken.AddAsync(It.IsAny<RefreshToken>()))
                .Callback<RefreshToken>(rt => createdRefreshToken = rt);

            _mockUnitOfWork.Setup(r => r.User.GetByUserNameAsync(existingToken.UserName))
                .ReturnsAsync(user);

            _mockUnitOfWork.Setup(u => u.SaveChangesAsync())
            .ThrowsAsync(new DbUpdateConcurrencyException());

            var ex = await Assert.ThrowsAsync<CxException>(() =>
            _userService.TryToRefreshTokenAsync(_mockHttpContext.Object.Request, _mockHttpContext.Object.Response, jwtKey, issuer));

            ex.ExceptionType.Should().Be(CxExceptionTypes.Unauthorized);
        }
    }
}
