using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public interface IAdministrativeAreaRepository : IReferenceDataEntityRepository<string, AdministrativeArea>
    {
        string BuildId(string countryId, string areaCode);
        Task<bool> ExistsIncludingDeletedAsync(string countryId, string areaCode, CancellationToken cancellationToken = default);
    }
}
