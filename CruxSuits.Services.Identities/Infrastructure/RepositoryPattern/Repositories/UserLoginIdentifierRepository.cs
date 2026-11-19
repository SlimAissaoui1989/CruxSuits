using CruxSuits.Services.Identities.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identities.Infrastructure.RepositoryPattern.Repositories
{
    public class UserLoginIdentifierRepository : IdentityRepository<int, UserLoginIdentifier>, IUserLoginIdentifierRepository
    {
        public UserLoginIdentifierRepository(IIdentityUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
