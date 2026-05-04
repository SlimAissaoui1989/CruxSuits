using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.PhoneTypeNames
{
    public class PhoneTypeNameRepository : CruxSuitsRepository<string, PhoneTypeName>, IPhoneTypeNameRepository
    {
        public PhoneTypeNameRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
