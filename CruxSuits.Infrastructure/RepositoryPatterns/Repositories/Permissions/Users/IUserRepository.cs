using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Infrastructure.RepositoryPatterns.Repositories.Permissions.Users
{
    public interface IUserRepository : ICruxSuitsRepository<int, User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByPhoneAsync(string phone);
        Task<User?> GetByUserNameAsync(string userName);
    }
}
