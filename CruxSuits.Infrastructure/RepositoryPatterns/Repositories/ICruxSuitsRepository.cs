using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories
{
    public interface ICruxSuitsRepository<TKey, TEntity> : IRepository<CruxSuitsUnitOfWork, TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
    }
}
