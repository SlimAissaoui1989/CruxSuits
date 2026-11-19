using CruxIT.Library.DataToolkit.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

internal sealed class CountryRepositoryFixture : IDisposable
{
    private readonly Mock<IReferenceDataUnitOfWork> _unitOfWork = new();
    private readonly Mock<CruxIT.Library.DataToolkit.RepositoryPattern.Repositories.ITraceRepository> _traceRepository = new();

    public CountryRepositoryFixture()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        Context = new ReferenceDataContext(options);
        var currencyRepository = new Mock<ICurrencyRepository>();
        currencyRepository
            .Setup(repository => repository.GetByIdOrDefaultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string id, CancellationToken _) =>
                Context.Currencies.Local.FirstOrDefault(currency => currency.Id == id)
                ?? Context.Currencies.FirstOrDefault(currency => currency.Id == id));

        _unitOfWork
            .As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(unitOfWork => unitOfWork.DbContext)
            .Returns(Context);
        _unitOfWork
            .As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(unitOfWork => unitOfWork.Trace)
            .Returns(_traceRepository.Object);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.DbContext).Returns(Context);
        _unitOfWork.Setup(unitOfWork => unitOfWork.Set<Country>()).Returns(Context.Countries);
        _unitOfWork.SetupGet(unitOfWork => unitOfWork.Currency).Returns(currencyRepository.Object);
        Context.Currencies.Add(new Currency { Id = "USD", NumericCode = "840", DecimalSeparator = 2 });

        Repository = new CountryRepository(_unitOfWork.Object);
    }

    public ReferenceDataContext Context { get; }
    public CountryRepository Repository { get; }

    public static Country CreateCountry(
        string id = "US",
        string alpha3Code = "USA",
        string numericCode = "840",
        string currencyId = "USD") => new()
    {
        Id = id,
        Alpha3Code = alpha3Code,
        NumericCode = numericCode,
        CurrencyId = currencyId
    };

    public void Dispose() => Context.Dispose();
}
