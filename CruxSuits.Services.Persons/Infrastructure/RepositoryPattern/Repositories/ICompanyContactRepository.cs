using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public interface ICompanyContactRepository : IPersonEntityRepository<int, CompanyContact>
    {
    }
}
