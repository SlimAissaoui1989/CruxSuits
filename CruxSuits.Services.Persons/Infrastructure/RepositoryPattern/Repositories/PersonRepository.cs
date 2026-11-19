using System.Text.RegularExpressions;
using PersonEntity = CruxSuits.Services.Persons.Infrastructure.EntityFramework.Entities.Person;

namespace CruxSuits.Services.Persons.Infrastructure.RepositoryPattern.Repositories
{
    public class PersonRepository : PersonBaseRepository<PersonEntity>, IPersonRepository
    {
        // Accept Unicode letters/combining marks in each name part, separated only by spaces, apostrophes, or hyphens.
        private static readonly Regex ValidNamePattern = new(
            @"^[\p{L}\p{M}]+(?:[ '\u2019-][\p{L}\p{M}]+)*$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public PersonRepository(IPersonUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task<PersonEntity> AddAsync(PersonEntity entity, CancellationToken cancellationToken = default)
        {
            ValidateAndPreparePerson(entity);
            return base.AddAsync(entity, cancellationToken);
        }

        public override Task<PersonEntity> UpdateAsync(PersonEntity entity, CancellationToken cancellationToken = default)
        {
            ValidateAndPreparePerson(entity);
            return base.UpdateAsync(entity, cancellationToken);
        }

        private static void ValidateAndPreparePerson(PersonEntity entity)
        {
            if (entity.BirthDate is { } birthDate && birthDate >= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(entity.BirthDate),
                    birthDate,
                    "Birth date must be earlier than today.");
            }

            var firstName = entity.FirstName?.Trim();
            var lastName = entity.LastName?.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || !ValidNamePattern.IsMatch(firstName))
            {
                throw new ArgumentException("First name is invalid.", nameof(entity.FirstName));
            }

            if (string.IsNullOrWhiteSpace(lastName) || !ValidNamePattern.IsMatch(lastName))
            {
                throw new ArgumentException("Last name is invalid.", nameof(entity.LastName));
            }

            entity.FirstName = firstName;
            entity.LastName = lastName;
            entity.FullName = $"{firstName} {lastName}";
        }
    }
}
