using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern
{
    public interface IReferenceDataUnitOfWork : IUnitOfWork<ReferenceDataContext>
    {
        IAdministrativeAreaRepository AdministrativeArea { get; }
        IAdministrativeAreaNameRepository AdministrativeAreaName { get; }
        ICountryRepository Country { get; }
        ICountryNameRepository CountryName { get; }
        ICurrencyRepository Currency { get; }
        ICurrencyNameRepository CurrencyName { get; }
        ILanguageRepository Language { get; }
        ILocalityRepository Locality { get; }
        ILocalityNameRepository LocalityName { get; }
    }
}
