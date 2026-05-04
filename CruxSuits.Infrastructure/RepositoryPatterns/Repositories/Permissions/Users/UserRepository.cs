using CruxIT.Library.DataToolkit.RepositoryPattern.Repositories;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Users
{
    public class UserRepository : CruxSuitsRepository<int, User>, IUserRepository
    {
        public UserRepository(CruxSuitsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await this.Include(i => i.Role)
                .Include(i => i.Person)
                .GetFirstOrDefaultAsync(c => c.IsEnabled
                    && c.Person!.EMails!.Any(c1 =>
                        c1.IsValid
                        && c1.EMailAddress == email));
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await this.Include(i => i.Role)
                .Include(i => i.Person)
                .GetFirstOrDefaultAsync(c => c.IsEnabled
                    && c.Person!.Phones!.Any(c1 =>
                        c1.IsValid
                        && c1.FullNumber == phone));
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await this.Include(i => i.Role)
                .Include(i => i.Person)
                .GetFirstOrDefaultAsync(c => c.IsEnabled && c.UserName == userName);
        }
    }
}
