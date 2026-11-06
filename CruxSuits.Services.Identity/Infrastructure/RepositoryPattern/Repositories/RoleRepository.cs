using CruxSuits.Services.Identity.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public class RoleRepository : IdentityRepository<int, Role>, IRoleRepository
    {
        public RoleRepository(IIdentityUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
