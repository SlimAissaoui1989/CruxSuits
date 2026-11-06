using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyLegalIdentityTypeRepository : PersonEntityRepository<string, CompanyLegalIdentityType>, ICompanyLegalIdentityTypeRepository
    {
        public CompanyLegalIdentityTypeRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
