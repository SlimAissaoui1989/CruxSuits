using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.Person.Infrastructure.EntityFramework;
using CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern
{
    public interface IPersonUnitOfWork : IUnitOfWork<PersonContext>
    {
        IAddressRepository Address { get; }
        ICompanyRepository Company { get; }
        ICompanyContactRepository CompanyContact { get; }
        ICompanyLegalIdentityRepository CompanyLegalIdentity { get; }
        ICompanyLegalIdentityTypeRepository CompanyLegalIdentityType { get; }
        IEMailRepository EMail { get; }
        IGenderRepository Gender { get; }
        IIBANRepository IBAN { get; }
        IPersonRepository Person { get; }
        IPersonIdentityRepository PersonIdentity { get; }
        IPersonIdentityTypeRepository PersonIdentityType { get; }
        IPhoneRepository Phone { get; }
        IPhoneTypeRepository PhoneType { get; }
    }
}
