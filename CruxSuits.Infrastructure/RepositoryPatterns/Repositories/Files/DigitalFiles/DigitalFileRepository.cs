using CruxSuits.Infrastructure.EntityFramework.Entities.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Files.DigitalFiles
{
    public class DigitalFileRepository : CruxSuitsRepository<Guid, DigitalFile>, IDigitalFileRepository
    {
        public DigitalFileRepository(CruxSuitsUnitOfWork unitOfWork)
            : base(unitOfWork )
        {
            UseTrace = false;
        }

        public override Task<DigitalFile> AddAsync(DigitalFile entity)
        {
            return base.AddAsync(entity);
        }
    }
}
