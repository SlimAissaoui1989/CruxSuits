using CruxIT.Library.DataToolkit.EntityFramework;
using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories
{
    public class CruxSuitsRepository<TKey, TEntity> : 
        Repository<CruxSuitsUnitOfWork, TKey, TEntity>,
        ICruxSuitsRepository<TKey, TEntity>
    where TKey : IEquatable<TKey> 
    where TEntity : class, IEntityBase<TKey>
    {
        public CruxSuitsRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
