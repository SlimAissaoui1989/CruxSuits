using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern
{
    public class ReferenceDataUnitOfWork : UnitOfWork<ReferenceDataContext>, IReferenceDataUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private IAdministrativeAreaRepository? _administrativeArea;
        private IAdministrativeAreaNameRepository? _administrativeAreaName;
        private ICountryRepository? _country;
        private ICountryNameRepository? _countryName;
        private ICurrencyRepository? _currency;
        private ICurrencyNameRepository? _currencyName;
        private ILanguageRepository? _language;
        private ILocalityRepository? _locality;
        private ILocalityNameRepository? _localityName;

        public ReferenceDataUnitOfWork(ReferenceDataContext context, IServiceProvider serviceProvider)
            : base(context)
        {
#if DEBUG
            //context.Database.EnsureCreated();
#endif
            _serviceProvider = serviceProvider;
        }

        public IAdministrativeAreaRepository AdministrativeArea =>
            _administrativeArea ??= _serviceProvider.GetRequiredService<IAdministrativeAreaRepository>();

        public IAdministrativeAreaNameRepository AdministrativeAreaName =>
            _administrativeAreaName ??= _serviceProvider.GetRequiredService<IAdministrativeAreaNameRepository>();

        public ICountryRepository Country =>
            _country ??= _serviceProvider.GetRequiredService<ICountryRepository>();

        public ICountryNameRepository CountryName =>
            _countryName ??= _serviceProvider.GetRequiredService<ICountryNameRepository>();

        public ICurrencyRepository Currency =>
            _currency ??= _serviceProvider.GetRequiredService<ICurrencyRepository>();

        public ICurrencyNameRepository CurrencyName =>
            _currencyName ??= _serviceProvider.GetRequiredService<ICurrencyNameRepository>();

        public ILanguageRepository Language =>
            _language ??= _serviceProvider.GetRequiredService<ILanguageRepository>();

        public ILocalityRepository Locality =>
            _locality ??= _serviceProvider.GetRequiredService<ILocalityRepository>();

        public ILocalityNameRepository LocalityName =>
            _localityName ??= _serviceProvider.GetRequiredService<ILocalityNameRepository>();

    }
}
