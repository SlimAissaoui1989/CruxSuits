using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;
using Microsoft.EntityFrameworkCore;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories
{
    public class LocalityRepository : ReferenceDataEntityRepository<string, Locality>, ILocalityRepository
    {
        public LocalityRepository(IReferenceDataUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public string BuildId(string administrativeAreaId, string localityCode) => $"{administrativeAreaId}-{localityCode}";

        public Task<List<LocalitySuggestionResponse>> AutocompleteAsync(
            string query, string languageId, CancellationToken cancellationToken = default)
        {
            var terms = query.ToUpperInvariant().Split([' ', '\t', '\r', '\n', ',', '-', '\'', '’'],
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Distinct()
                .ToArray();
            if (terms.Length == 0)
                return Task.FromResult(new List<LocalitySuggestionResponse>());

            var localities = DbSet.AsNoTracking().Where(locality =>
                locality.AdministrativeArea != null && !locality.AdministrativeArea.IsDeleted &&
                locality.AdministrativeArea.Country != null && !locality.AdministrativeArea.Country.IsDeleted);

            var ranked = localities.Select(locality => new { Locality = locality, Score = 0 });
            foreach (var term in terms)
            {
                var prefix = " " + term;
                var exact = prefix + " ";
                ranked = ranked
                    .Select(candidate => new
                    {
                        candidate.Locality,
                        candidate.Score,
                        Match = candidate.Locality.Names.Any(name => (" " + name.Name.ToUpper().Replace('-', ' ').Replace('\'', ' ').Replace('’', ' ') + " ").Contains(exact)) ? 6
                            : candidate.Locality.Names.Any(name => (" " + name.Name.ToUpper().Replace('-', ' ').Replace('\'', ' ').Replace('’', ' ') + " ").Contains(prefix)) ? 5
                            : candidate.Locality.AdministrativeArea!.Names.Any(name => (" " + name.Name.ToUpper().Replace('-', ' ').Replace('\'', ' ').Replace('’', ' ') + " ").Contains(exact)) ? 4
                            : candidate.Locality.AdministrativeArea!.Names.Any(name => (" " + name.Name.ToUpper().Replace('-', ' ').Replace('\'', ' ').Replace('’', ' ') + " ").Contains(prefix)) ? 3
                            : candidate.Locality.AdministrativeArea!.Country!.Names.Any(name => (" " + name.Name.ToUpper().Replace('-', ' ').Replace('\'', ' ').Replace('’', ' ') + " ").Contains(exact)) ? 2
                            : candidate.Locality.AdministrativeArea!.Country!.Names.Any(name => (" " + name.Name.ToUpper().Replace('-', ' ').Replace('\'', ' ').Replace('’', ' ') + " ").Contains(prefix)) ? 1
                            : 0
                    })
                    .Where(candidate => candidate.Match > 0)
                    .Select(candidate => new { candidate.Locality, Score = candidate.Score + candidate.Match });
            }

            return ranked
                .OrderByDescending(candidate => candidate.Score)
                .ThenBy(candidate => candidate.Locality.Id)
                .Take(10)
                .Select(candidate => candidate.Locality)
                .Select(locality => new LocalitySuggestionResponse
                {
                    Id = locality.Id,
                    Code = locality.Code,
                    Name = locality.Names.OrderByDescending(name => name.LanguageId == languageId)
                        .ThenByDescending(name => name.LanguageId == "en").ThenBy(name => name.LanguageId)
                        .Select(name => name.Name).FirstOrDefault() ?? locality.Code,
                    AdministrativeAreaId = locality.AdministrativeAreaId,
                    AdministrativeAreaCode = locality.AdministrativeArea!.Code,
                    AdministrativeAreaName = locality.AdministrativeArea.Names.OrderByDescending(name => name.LanguageId == languageId)
                        .ThenByDescending(name => name.LanguageId == "en").ThenBy(name => name.LanguageId)
                        .Select(name => name.Name).FirstOrDefault() ?? locality.AdministrativeArea.Code,
                    CountryId = locality.AdministrativeArea.CountryId,
                    CountryName = locality.AdministrativeArea.Country!.Names.OrderByDescending(name => name.LanguageId == languageId)
                        .ThenByDescending(name => name.LanguageId == "en").ThenBy(name => name.LanguageId)
                        .Select(name => name.Name).FirstOrDefault() ?? locality.AdministrativeArea.CountryId
                })
                .ToListAsync(cancellationToken);
        }

        public override Task<Locality> AddAsync(Locality entity, CancellationToken cancellationToken = default)
        {
            entity.Id = BuildId(entity.AdministrativeAreaId, entity.Code);
            return base.AddAsync(entity, cancellationToken);
        }

        public Task<bool> ExistsIncludingDeletedAsync(
            string administrativeAreaId, string localityCode, CancellationToken cancellationToken = default) =>
            DbSet.IgnoreQueryFilters().AnyAsync(
                locality => locality.AdministrativeAreaId == administrativeAreaId && locality.Code == localityCode,
                cancellationToken);
    }
}
