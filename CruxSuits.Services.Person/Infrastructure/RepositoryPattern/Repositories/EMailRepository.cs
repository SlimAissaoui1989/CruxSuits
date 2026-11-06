using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class EMailRepository : PersonEntityRepository<int, EMail>, IEMailRepository
    {
        public EMailRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
