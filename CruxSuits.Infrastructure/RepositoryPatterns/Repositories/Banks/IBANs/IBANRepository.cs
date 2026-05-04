using CruxSuits.Infrastructure.EntityFramework.Entities.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Banks.IBANs
{
    public class IBANRepository : CruxSuitsRepository<int, IBAN>, IIBANRepository
    {
        public IBANRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
