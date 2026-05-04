using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Files.DigitalFileTraces
{
    public class DigitalFileTraceRepository : CruxSuitsRepository<int, DigitalFileTrace>, IDigitalFileTraceRepository
    {
        public DigitalFileTraceRepository(CruxSuitsUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
