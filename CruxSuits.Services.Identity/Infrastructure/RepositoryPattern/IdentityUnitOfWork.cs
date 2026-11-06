using AutoMapper;
using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Configurations;
using CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern
{
    public class IdentityUnitOfWork : UnitOfWork<IdentityContext>, IIdentityUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        private IRefreshTokenRepository? _refreshToken;
        private IRoleRepository? _role;
        private IUserRepository? _user;
        private IUserLoginIdentifierRepository? _userLoginIdentifier;

        public IdentityUnitOfWork(IdentityContext context, IServiceProvider serviceProvider) : base(context)
        {
#if DEBUG
            context.Database.EnsureCreated();
#endif
            _serviceProvider = serviceProvider;
        }

        public IRefreshTokenRepository RefreshToken => _refreshToken ??= _serviceProvider.GetRequiredService<IRefreshTokenRepository>();

        public IRoleRepository Role => _role ??= _serviceProvider.GetRequiredService<IRoleRepository>();

        public IUserRepository User => _user ??= _serviceProvider.GetRequiredService<IUserRepository>();

        public IUserLoginIdentifierRepository UserLoginIdentifier => _userLoginIdentifier ??= _serviceProvider.GetRequiredService<IUserLoginIdentifierRepository>();
    }
}
