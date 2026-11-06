using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern
{
    public interface IIdentityUnitOfWork : IUnitOfWork<IdentityContext>
    {
        IRefreshTokenRepository RefreshToken { get; }
        IRoleRepository Role { get; }
        IUserRepository User { get; }
        IUserLoginIdentifierRepository UserLoginIdentifier { get; }
    }
}
