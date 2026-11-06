using CruxIT.Library.DataToolkit.EntityFramework.BaseTypes;
using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories
{
    public class ReferenceDataEntityRepository<TKey, TEntity> : Repository<IReferenceDataUnitOfWork, TKey, TEntity>, IReferenceDataEntityRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntityBase<TKey>
    {
        public ReferenceDataEntityRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
