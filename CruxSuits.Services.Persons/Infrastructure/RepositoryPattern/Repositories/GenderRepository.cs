using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class GenderRepository : PersonEntityRepository<string, Gender>, IGenderRepository
    {
        public GenderRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
