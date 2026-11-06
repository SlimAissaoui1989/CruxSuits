using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public interface IPersonIdentityTypeRepository : IPersonEntityRepository<string, PersonIdentityType>
    {
    }
}
