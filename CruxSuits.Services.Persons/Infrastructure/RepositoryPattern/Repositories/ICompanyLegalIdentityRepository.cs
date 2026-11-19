using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public interface ICompanyLegalIdentityRepository : IPersonEntityRepository<int, CompanyLegalIdentity>
    {
    }
}
