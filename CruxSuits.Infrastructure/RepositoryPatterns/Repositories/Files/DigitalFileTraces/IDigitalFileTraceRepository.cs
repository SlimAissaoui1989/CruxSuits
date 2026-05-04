using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Files.DigitalFileTraces
{
    public interface IDigitalFileTraceRepository : ICruxSuitsRepository<int, DigitalFileTrace>
    {
    }
}
