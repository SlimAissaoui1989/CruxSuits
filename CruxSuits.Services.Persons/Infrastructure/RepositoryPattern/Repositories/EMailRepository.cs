using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class EMailRepository : PersonEntityRepository<int, EMail>, IEMailRepository
    {
        public EMailRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
