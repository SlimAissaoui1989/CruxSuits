using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyRepository : PersonEntityRepository<Guid, Company>, ICompanyRepository
    {
        public CompanyRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
