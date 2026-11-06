using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern
{
    public interface IReferenceDataUnitOfWork : IUnitOfWork<ReferenceDataContext>
    {
        ICountryRepository Country { get; }
        ICurrencyRepository Currency { get; }
        ILanguageRepository Language { get; }
        ILocalizedNameRepository LocalizedName { get; }
    }
}
