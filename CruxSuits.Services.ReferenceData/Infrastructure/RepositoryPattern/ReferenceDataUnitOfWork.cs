using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.ReferenceData.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CruxSuits.Services.ReferenceData.Infrastructure.RepositoryPattern
{
    public class ReferenceDataUnitOfWork : UnitOfWork<ReferenceDataContext>, IReferenceDataUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private ICountryRepository? _country;
        private ICurrencyRepository? _currency;
        private ILanguageRepository? _language;
        private ILocalizedNameRepository? _localizedName;

        public ReferenceDataUnitOfWork(ReferenceDataContext context, IServiceProvider serviceProvider)
            : base(context)
        {
#if DEBUG
            context.Database.EnsureCreated();
#endif
            _serviceProvider = serviceProvider;
        }

        public ICountryRepository Country =>
            _country ??= _serviceProvider.GetRequiredService<ICountryRepository>();

        public ICurrencyRepository Currency =>
            _currency ??= _serviceProvider.GetRequiredService<ICurrencyRepository>();

        public ILanguageRepository Language =>
            _language ??= _serviceProvider.GetRequiredService<ILanguageRepository>();

        public ILocalizedNameRepository LocalizedName =>
            _localizedName ??= _serviceProvider.GetRequiredService<ILocalizedNameRepository>();
    }
}
