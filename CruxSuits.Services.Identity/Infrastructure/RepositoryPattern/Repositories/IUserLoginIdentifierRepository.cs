using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public interface IUserLoginIdentifierRepository : IIdentityRepository<int, UserLoginIdentifier>
    {
    }
}
