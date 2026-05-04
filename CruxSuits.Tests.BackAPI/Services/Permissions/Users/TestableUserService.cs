using CruxSuits.BackAPI.Models;
using CruxSuits.BackAPI.Services.Permissions.Users;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.BackAPI.Services.Permissions.Users
{
    public class TestableUserService : UserService
    {
        public TestableUserService(ICruxSuitsUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, ILogger<UserService> logger) : base(unitOfWork, passwordHasher, logger)
        {
        }

        public (string connectionId, Guid deviceId, string accessToken, string oldToken) CallExtractRequestDataOrThrow(HttpRequest request)
        => ExtractRequestDataOrThrow(request);

        public Task<RefreshToken> CallGetValidExistingRefreshTokenOrThrowAsync(string oldToken, string accessToken)
            => GetValidExistingRefreshTokenOrThrowAsync(oldToken, accessToken);

        public RefreshTokenConnections CallValidateConnectionOrThrow(string oldToken, string connectionId, RefreshToken existingToken)
            => ValidateConnectionOrThrow(oldToken, connectionId, existingToken);

        public Task<User> CallGetValidUserOrThrowAsync(RefreshToken existingToken)
            => GetValidUserOrThrowAsync(existingToken);
    }
}
