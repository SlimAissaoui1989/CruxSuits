using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.EntityFramework.Entities.Contacts;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Infrastructure.EntityFramework.Entities.Persons;
using CruxSuits.Infrastructure.RepositoryPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.Users
{
    public static class UserData
    {
        private static User TestUser => new User
        {
            Id = 1,
            UserName = "test.User",
            IsEnabled = true,
            Password = "123",
            Role = new Role { Level = "A", Name = "testRole", },
            Person = new Person
            {
                FirstName = "test",
                LastName = "user",
                FullName = "test user",
                EMails = new List<EMail>
                    {
                        new EMail { EMailAddress = "user@example.com", IsValid = true }
                    },
                Phones = new List<Phone>
                    {
                        new Phone { Number = "123456", CountryId="TN", PhoneTypeId = "Mob", FullNumber = "123456", IsValid = true }
                    },
            }
        };

        public static async Task InitializeTestUserAsync(CruxSuitsUnitOfWork uow, bool isEnabled = true, bool emailValid = true, bool phoneValid = true)
        {
            User test = TestUser;

            test.IsEnabled = isEnabled;
            test.Person!.EMails!.First().IsValid = emailValid;
            test.Person!.Phones!.First().IsValid = phoneValid;
            await uow.User.AddAsync(test);
            await uow.SaveChangesAsync();
        }
    }
}
