using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class GenderRepository : PersonEntityRepository<string, Gender>, IGenderRepository
    {
        public GenderRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
