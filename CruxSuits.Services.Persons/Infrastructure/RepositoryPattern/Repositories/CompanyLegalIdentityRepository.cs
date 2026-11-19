using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyLegalIdentityRepository : PersonEntityRepository<int, CompanyLegalIdentity>, ICompanyLegalIdentityRepository
    {
        public CompanyLegalIdentityRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
