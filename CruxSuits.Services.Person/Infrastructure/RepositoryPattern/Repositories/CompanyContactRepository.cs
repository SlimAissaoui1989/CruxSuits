using CruxSuits.Services.Person.Infrastructure.EntityFramework.Entities;

namespace CruxSuits.Services.Person.Infrastructure.RepositoryPattern.Repositories
{
    public class CompanyContactRepository : PersonEntityRepository<int, CompanyContact>, ICompanyContactRepository
    {
        public CompanyContactRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
