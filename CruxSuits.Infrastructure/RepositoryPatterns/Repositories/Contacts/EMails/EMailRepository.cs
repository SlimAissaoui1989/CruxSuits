using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Contacts.EMails
{
    public class EMailRepository : CruxSuitsRepository<int, EMail>, IEMailRepository
    {
        public EMailRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
