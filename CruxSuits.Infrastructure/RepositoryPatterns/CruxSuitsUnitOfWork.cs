using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Banks.IBANs;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.EMails;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.Phones;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.PhoneTypeNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.PhoneTypes;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Files.DigitalFiles;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Files.DigitalFileTraces;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Adresses;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Countries;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.CountryNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.Currencies;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.GeoLocations.CurrencyNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Languages.Languages;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldGeneralPermissions;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByRoleDetails;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByRoles;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByUserDetails;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFieldPermissionByUsers;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Fields.DataFields;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectGeneralPermissions;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByRoleDetails;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByRoles;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByUserDetails;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjectPermissionByUsers;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Datas.Objects.DataObjects;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.RefreshTokens;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Roles;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.RoleUiResourcePermissions;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourceNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourcePermissionBases;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResources;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourceTypeNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UiResourceTypes;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.UiResources.UserUiResourcePermissions;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Users;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.GenderNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.Genders;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.Peoples;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentities;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentityTypeNames;
using CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Persons.PersonIdentityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns
{
    public class CruxSuitsUnitOfWork : UnitOfWork<CruxSuitsContext>, ICruxSuitsUnitOfWork
    {
        public CruxSuitsUnitOfWork(CruxSuitsContext context) : base(context)
        {
#if DEBUG
            context.Database.EnsureCreated();
#endif
        }

        //Banks
        public IIBANRepository IBAN => GetRepository<IBANRepository>();

        //Contacts
        public IEMailRepository EMail => GetRepository<EMailRepository>();
        public IPhoneRepository Phone => GetRepository<PhoneRepository>();
        public IPhoneTypeNameRepository PhoneTypeName => GetRepository<PhoneTypeNameRepository>();
        public IPhoneTypeRepository PhoneType => GetRepository<PhoneTypeRepository>();

        //Files
        public IDigitalFileRepository DigitalFile => GetRepository<DigitalFileRepository>();
        public IDigitalFileTraceRepository DigitalFileTrace => GetRepository<DigitalFileTraceRepository>();

        //GeoLocations
        public IAddressRepository Address => GetRepository<AddressRepository>();
        public ICountryRepository Country => GetRepository<CountryRepository>();
        public ICountryNameRepository CountryName => GetRepository<CountryNameRepository>();
        public ICurrencyRepository Currency => GetRepository<CurrencyRepository>();
        public ICurrencyNameRepository CurrencyName => GetRepository<CurrencyNameRepository>();

        //Langages
        public ILanguageRepository Language => GetRepository<LanguageRepository>();

        //Permissions
        public IRefreshTokenRepository RefreshToken => GetRepository<RefreshTokenRepository>();
        public IRoleRepository Role => GetRepository<RoleRepository>();
        public IUserRepository User => GetRepository<UserRepository>();

        //Permissions.Datas.Fields
        public IDataFieldGeneralPermissionRepository DataFieldGeneralPermission => GetRepository<DataFieldGeneralPermissionRepository>();
        public IDataFieldPermissionByRoleDetailRepository DataFieldPermissionByRoleDetail => GetRepository<DataFieldPermissionByRoleDetailRepository>();
        public IDataFieldPermissionByRoleRepository DataFieldPermissionByRole => GetRepository<DataFieldPermissionByRoleRepository>();
        public IDataFieldPermissionByUserDetailRepository DataFieldPermissionByUserDetail => GetRepository<DataFieldPermissionByUserDetailRepository>();
        public IDataFieldPermissionByUserRepository DataFieldPermissionByUser => GetRepository<DataFieldPermissionByUserRepository>();
        public IDataFieldRepository DataField => GetRepository<DataFieldRepository>();

        //Permissions.Datas.Object
        public IDataObjectGeneralPermissionRepository DataObjectGeneralPermission => GetRepository<DataObjectGeneralPermissionRepository>();
        public IDataObjectPermissionByRoleDetailRepository DataObjectPermissionByRoleDetail => GetRepository<DataObjectPermissionByRoleDetailRepository>();
        public IDataObjectPermissionByRoleRepository DataObjectPermissionByRole => GetRepository<DataObjectPermissionByRoleRepository>();
        public IDataObjectPermissionByUserDetailRepository DataObjectPermissionByUserDetail => GetRepository<DataObjectPermissionByUserDetailRepository>();
        public IDataObjectPermissionByUserRepository DataObjectPermissionByUser => GetRepository<DataObjectPermissionByUserRepository>();
        public IDataObjectRepository DataObject => GetRepository<DataObjectRepository>();

        //Permissions.Datas.UiResources 
        public IRoleUiResourcePermissionRepository RoleUiResourcePermission => GetRepository<RoleUiResourcePermissionRepository>();
        public IUiResourceNameRepository UiResourceName => GetRepository<UiResourceNameRepository>();
        public IUiResourceRepository UiResource => GetRepository<UiResourceRepository>();
        public IUiResourceTypeNameRepository UiResourceTypeName => GetRepository<UiResourceTypeNameRepository>();
        public IUiResourceTypeRepository UiResourceType => GetRepository<UiResourceTypeRepository>();
        public IUserUiResourcePermissionRepository UserUiResourcePermission => GetRepository<UserUiResourcePermissionRepository>();

        //Persons
        public IGenderNameRepository GenderName => GetRepository<GenderNameRepository>();
        public IGenderRepository Gender => GetRepository<GenderRepository>();
        public IPersonRepository Person => GetRepository<PersonRepository>();
        public IPersonIdentityRepository PersonIdentity => GetRepository<PersonIdentityRepository>();
        public IPersonIdentityTypeNameRepository PersonIdentityTypeName => GetRepository<PersonIdentityTypeNameRepository>();
        public IPersonIdentityTypeRepository PersonIdentityType => GetRepository<PersonIdentityTypeRepository>();



    }
}
