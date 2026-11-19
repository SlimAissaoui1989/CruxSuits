using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyRepository : PersonBaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
