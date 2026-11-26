using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern
{
    public class ReferenceDataUnitOfWork : UnitOfWork<ReferenceDataContext>, IReferenceDataUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private ICountryRepository? _country;
        private ICountryNameRepository? _countryName;
        private ICurrencyRepository? _currency;
        private ICurrencyNameRepository? _currencyName;
        private ILanguageRepository? _language;

        public ReferenceDataUnitOfWork(ReferenceDataContext context, IServiceProvider serviceProvider)
            : base(context)
        {
#if DEBUG
            //context.Database.EnsureCreated();
#endif
            _serviceProvider = serviceProvider;
        }

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

    }
}
