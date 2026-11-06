using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public interface IUserRepository : IIdentityRepository<Guid, User>
    {
    }
}
