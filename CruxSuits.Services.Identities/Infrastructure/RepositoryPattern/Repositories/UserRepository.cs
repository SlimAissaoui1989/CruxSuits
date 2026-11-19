using CruxSuits.Services.Identities.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identities.Infrastructure.RepositoryPattern.Repositories
{
    public class UserRepository : IdentityRepository<Guid, User>, IUserRepository
    {
        public UserRepository(IIdentityUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
