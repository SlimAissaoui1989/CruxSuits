using CruxSuits.Infrastructure.EntityFramework.Entities.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Banks.IBANs
{
    public interface IIBANRepository:ICruxSuitsRepository<int, IBAN>
    {
    }
}
