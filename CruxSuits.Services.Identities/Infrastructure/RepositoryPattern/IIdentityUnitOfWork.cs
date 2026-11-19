using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Identities.Infrastructure.EntityFramework;
using CruxSuits.Services.Identities.Infrastructure.RepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identities.Infrastructure.RepositoryPattern
{
    public interface IIdentityUnitOfWork : IUnitOfWork<IdentityContext>
    {
        IRefreshTokenRepository RefreshToken { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
        IUserLoginIdentifierRepository UserLoginIdentifier { get; }
    }
}
