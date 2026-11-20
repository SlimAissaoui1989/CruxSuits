using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

internal sealed class CountryNameRepositoryFixture : IDisposable
{
    private readonly ReferenceDataContext _context;

    public CountryNameRepositoryFixture()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        _context = new ReferenceDataContext(options);

        var traceRepository = new Mock<CruxIT.Library.DataToolkit.RepositoryPattern.Repositories.ITraceRepository>();
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(work => work.DbContext).Returns(_context);
        unitOfWork.As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(work => work.Trace).Returns(traceRepository.Object);
        unitOfWork.SetupGet(work => work.DbContext).Returns(_context);
        unitOfWork.Setup(work => work.Set<CountryName>()).Returns(_context.CountryNames);

        Repository = new CountryNameRepository(unitOfWork.Object);
    }

    public CountryNameRepository Repository { get; }
    public ReferenceDataContext Context => _context;

    public void Dispose() => _context.Dispose();
}
