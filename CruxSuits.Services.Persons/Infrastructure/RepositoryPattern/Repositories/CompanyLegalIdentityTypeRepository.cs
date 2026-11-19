using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyLegalIdentityTypeRepository : PersonEntityRepository<string, CompanyLegalIdentityType>, ICompanyLegalIdentityTypeRepository
    {
        public CompanyLegalIdentityTypeRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
