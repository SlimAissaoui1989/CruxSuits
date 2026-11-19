using CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyContactRepository : PersonEntityRepository<int, CompanyContact>, ICompanyContactRepository
    {
        public CompanyContactRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
