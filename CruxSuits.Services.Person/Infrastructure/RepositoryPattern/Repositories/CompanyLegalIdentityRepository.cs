using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyLegalIdentityRepository : PersonEntityRepository<int, CompanyLegalIdentity>, ICompanyLegalIdentityRepository
    {
        public CompanyLegalIdentityRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
