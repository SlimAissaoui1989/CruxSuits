using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class AdministrativeAreaRepository : ReferenceDataEntityRepository<string, AdministrativeArea>, IAdministrativeAreaRepository
    {
        public AdministrativeAreaRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public string BuildId(string countryId, string areaCode) => $"{countryId}-{areaCode}";

        public override Task<AdministrativeArea> AddAsync(AdministrativeArea entity, CancellationToken cancellationToken = default)
        {
            entity.Id = BuildId(entity.CountryId, entity.Code);
            return base.AddAsync(entity, cancellationToken);
        }

        public Task<bool> ExistsIncludingDeletedAsync(
            string countryId, string areaCode, CancellationToken cancellationToken = default) =>
            DbSet.IgnoreQueryFilters().AnyAsync(
                area => area.CountryId == countryId && area.Code == areaCode,
                cancellationToken);
    }
}
