using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public interface IPhoneRepository : IPersonEntityRepository<int, Phone>
    {
    }
}
