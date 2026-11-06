using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public class IdentityRepository<TKey, TEntity> : Repository<IIdentityUnitOfWork, TKey, TEntity>, IRepository<IIdentityUnitOfWork, TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
        public IdentityRepository(IIdentityUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
