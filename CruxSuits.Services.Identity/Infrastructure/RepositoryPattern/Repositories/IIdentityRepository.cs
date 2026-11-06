using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Services.Identity.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CruxSuits.Services.Identity.Infrastructure.RepositoryPattern.Repositories
{
    public interface IIdentityRepository<TKey, TEntity> : IRepository<IIdentityUnitOfWork, TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
    }
}
