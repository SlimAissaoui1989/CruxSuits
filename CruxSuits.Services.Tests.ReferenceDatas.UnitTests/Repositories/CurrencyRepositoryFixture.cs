using AutoMapper;
using CruxIT.Library.DataToolkit.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using CruxSuits.Services.ReferenceDatas.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

internal sealed class CurrencyRepositoryFixture : IDisposable
{
    private readonly ReferenceDataContext _context;

    public CurrencyRepositoryFixture()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        _context = new ReferenceDataContext(options);

        var mapper = new MapperConfiguration(
            configuration => configuration.AddProfile<CurrencyProfile>(),
            NullLoggerFactory.Instance)
            .CreateMapper();
        var traceRepository = new Mock<CruxIT.Library.DataToolkit.RepositoryPattern.Repositories.ITraceRepository>();
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.As<IUnitOfWork>()
            .SetupGet(work => work.DbContext)
            .Returns(_context);
        unitOfWork.As<IUnitOfWork>()
            .SetupGet(work => work.Trace)
            .Returns(traceRepository.Object);
        unitOfWork.SetupGet(work => work.DbContext).Returns(_context);
        unitOfWork.SetupGet(work => work.Mapper).Returns(mapper);
        unitOfWork.Setup(work => work.Set<Currency>()).Returns(_context.Currencies);

        Repository = new CurrencyRepository(unitOfWork.Object);
    }

    public CurrencyRepository Repository { get; }
    public ReferenceDataContext Context => _context;

    public async Task AddCurrencyAsync(string id, string numericCode, params (string LanguageId, string Name)[] names)
    {
        _context.Languages.AddRange(
            new Language { Id = "en", EnglishName = "English", NativeName = "English", IsRightToLeft = false },
            new Language { Id = "fr", EnglishName = "French", NativeName = "Français", IsRightToLeft = false });
        _context.Currencies.Add(new Currency
        {
            Id = id,
            NumericCode = numericCode,
            CreatedDate = DateTime.UtcNow,
            IsDeleted = false
        });
        _context.CurrencyNames.AddRange(names.Select((name, index) => new CurrencyName
        {
            Id = index + 50000,
            CurrencyId = id,
            LanguageId = name.LanguageId,
            Name = name.Name
        }));
        await _context.SaveChangesAsync();
    }

    public void Dispose() => _context.Dispose();
}
