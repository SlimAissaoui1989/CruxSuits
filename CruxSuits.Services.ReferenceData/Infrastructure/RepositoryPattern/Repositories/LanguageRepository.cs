using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories
{
    public class LanguageRepository : ReferenceDataEntityRepository<string, Language>, ILanguageRepository
    {
        public LanguageRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
