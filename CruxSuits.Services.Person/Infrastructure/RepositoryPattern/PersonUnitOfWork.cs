using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Person.Infrastructure.EntityFramework;
using CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern
{
    public class PersonUnitOfWork : UnitOfWork<PersonContext>, IPersonUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        private IAddressRepository? _Address;
        private ICompanyRepository? _Company;
        private ICompanyContactRepository? _CompanyContact;
        private ICompanyLegalIdentityRepository? _CompanyLegalIdentity;
        private ICompanyLegalIdentityTypeRepository? _CompanyLegalIdentityType;
        private IEMailRepository? _EMail;
        private IGenderRepository? _Gender;
        private IIBANRepository? _IBAN;
        private IPersonRepository? _Person;
        private IPersonIdentityRepository? _PersonIdentity;
        private IPersonIdentityTypeRepository? _PersonIdentityType;
        private IPhoneRepository? _Phone;
        private IPhoneTypeRepository? _PhoneType;

        public PersonUnitOfWork(PersonContext context, IServiceProvider serviceProvider) : base(context)
        {
#if DEBUG
            context.Database.EnsureCreated();
#endif
            _serviceProvider = serviceProvider;
        }

        public IAddressRepository Address => _Address ??= _serviceProvider.GetRequiredService<IAddressRepository>();
        public ICompanyRepository Company => _Company ??= _serviceProvider.GetRequiredService<ICompanyRepository>();
        public ICompanyContactRepository CompanyContact => _CompanyContact ??= _serviceProvider.GetRequiredService<ICompanyContactRepository>();
        public ICompanyLegalIdentityRepository CompanyLegalIdentity => _CompanyLegalIdentity ??= _serviceProvider.GetRequiredService<ICompanyLegalIdentityRepository>();
        public ICompanyLegalIdentityTypeRepository CompanyLegalIdentityType => _CompanyLegalIdentityType ??= _serviceProvider.GetRequiredService<ICompanyLegalIdentityTypeRepository>();
        public IEMailRepository EMail => _EMail ??= _serviceProvider.GetRequiredService<IEMailRepository>();
        public IGenderRepository Gender => _Gender ??= _serviceProvider.GetRequiredService<IGenderRepository>();
        public IIBANRepository IBAN => _IBAN ??= _serviceProvider.GetRequiredService<IIBANRepository>();
        public IPersonRepository Person => _Person ??= _serviceProvider.GetRequiredService<IPersonRepository>();
        public IPersonIdentityRepository PersonIdentity => _PersonIdentity ??= _serviceProvider.GetRequiredService<IPersonIdentityRepository>();
        public IPersonIdentityTypeRepository PersonIdentityType => _PersonIdentityType ??= _serviceProvider.GetRequiredService<IPersonIdentityTypeRepository>();
        public IPhoneRepository Phone => _Phone ??= _serviceProvider.GetRequiredService<IPhoneRepository>();
        public IPhoneTypeRepository PhoneType => _PhoneType ??= _serviceProvider.GetRequiredService<IPhoneTypeRepository>();
    }
}
