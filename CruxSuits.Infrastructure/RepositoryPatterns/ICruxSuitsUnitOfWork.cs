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
    public interface ICruxSuitsUnitOfWork : IUnitOfWork<CruxSuitsContext>
    {
        //Banks
        public IIBANRepository IBAN { get ;}

        //Contacts
        public IEMailRepository EMail { get ;}
        public IPhoneRepository Phone { get ;}
        public IPhoneTypeNameRepository PhoneTypeName { get ;}
        public IPhoneTypeRepository PhoneType { get ;}

        //Files
        public IDigitalFileRepository DigitalFile { get ;}
        public IDigitalFileTraceRepository DigitalFileTrace { get ;}

        //GeoLocations
        public IAddressRepository Address { get ;}
        public ICountryRepository Country { get ;}
        public ICountryNameRepository CountryName { get ;}
        public ICurrencyRepository Currency { get ;}
        public ICurrencyNameRepository CurrencyName { get ;}

        //Langages
        public ILanguageRepository Language { get ;}

        //Permissions
        public IRefreshTokenRepository RefreshToken { get ;}
        public IRoleRepository Role { get ;}
        public IUserRepository User { get ;}

        //Permissions.Datas.Fields
        public IDataFieldGeneralPermissionRepository DataFieldGeneralPermission { get ;}
        public IDataFieldPermissionByRoleDetailRepository DataFieldPermissionByRoleDetail { get ;}
        public IDataFieldPermissionByRoleRepository DataFieldPermissionByRole { get ;}
        public IDataFieldPermissionByUserDetailRepository DataFieldPermissionByUserDetail { get ;}
        public IDataFieldPermissionByUserRepository DataFieldPermissionByUser { get ;}
        public IDataFieldRepository DataField { get ;}

        //Permissions.Datas.Object
        public IDataObjectGeneralPermissionRepository DataObjectGeneralPermission { get ;}
        public IDataObjectPermissionByRoleDetailRepository DataObjectPermissionByRoleDetail { get ;}
        public IDataObjectPermissionByRoleRepository DataObjectPermissionByRole { get ;}
        public IDataObjectPermissionByUserDetailRepository DataObjectPermissionByUserDetail { get ;}
        public IDataObjectPermissionByUserRepository DataObjectPermissionByUser { get ;}
        public IDataObjectRepository DataObject { get ;}

        //Permissions.Datas.UiResources 
        public IRoleUiResourcePermissionRepository RoleUiResourcePermission { get ;}
        public IUiResourceNameRepository UiResourceName { get ;}
        public IUiResourceRepository UiResource { get ;}
        public IUiResourceTypeNameRepository UiResourceTypeName { get ;}
        public IUiResourceTypeRepository UiResourceType { get ;}
        public IUserUiResourcePermissionRepository UserUiResourcePermission { get ;}

        //Persons
        public IGenderNameRepository GenderName { get ;}
        public IGenderRepository Gender { get ;}
        public IPersonRepository Person { get ;}
        public IPersonIdentityRepository PersonIdentity { get ;}
        public IPersonIdentityTypeNameRepository PersonIdentityTypeName { get ;}
        public IPersonIdentityTypeRepository PersonIdentityType { get ;}
    }
}
