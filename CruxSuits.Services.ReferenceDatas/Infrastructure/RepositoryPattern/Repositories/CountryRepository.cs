using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class CountryRepository : ReferenceDataEntityRepository<string, Country>, ICountryRepository
    {
        public CountryRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public async Task<List<TResult>> GetAllLocalizedAsync<TResult>(
            string languageId,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .AsNoTracking()
                .ProjectTo<TResult>(
                    UnitOfWork!.Mapper!.ConfigurationProvider,
                    new { requestedLanguageId = languageId })
                .ToListAsync(cancellationToken);
        }

    }
}
