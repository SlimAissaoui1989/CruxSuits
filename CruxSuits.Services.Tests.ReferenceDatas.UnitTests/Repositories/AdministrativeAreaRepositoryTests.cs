using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern;
using CruxSuits.Services.ReferenceDatas.Infrastructure.RepositoryPattern.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CruxSuits.Services.ReferenceDatas.Tests.UnitTests.Repositories;

public sealed class AdministrativeAreaRepositoryTests
{
    // Contract check: verifies the repository builds a country-scoped ID and reserves deleted identifiers;
    // A pass confirms that the same ID remains reserved after soft deletion;
    // Failure signals that the ID format or duplicate protection regressed.
    [Fact]
    public async Task AddAsync_BuildsIdAndReservesItAfterSoftDeletion()
    {
        var options = new DbContextOptionsBuilder<ReferenceDataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
            .Options;
        await using var context = new ReferenceDataContext(options);
        var unitOfWork = new Mock<IReferenceDataUnitOfWork>();
        unitOfWork.As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(work => work.DbContext).Returns(context);
        unitOfWork.As<CruxIT.Library.DataToolkit.RepositoryPattern.IUnitOfWork>()
            .SetupGet(work => work.Trace)
            .Returns(Mock.Of<CruxIT.Library.DataToolkit.RepositoryPattern.Repositories.ITraceRepository>());
        unitOfWork.SetupGet(work => work.DbContext).Returns(context);
        unitOfWork.Setup(work => work.Set<AdministrativeArea>()).Returns(context.AdministrativeAreas);
        var repository = new AdministrativeAreaRepository(unitOfWork.Object);

        var area = new AdministrativeArea { CountryId = "TN", Code = "SF" };
        await repository.AddAsync(area);
        await context.SaveChangesAsync();

        Assert.Equal("TN-SF", area.Id);
        Assert.True(await context.AdministrativeAreas.AnyAsync(existing => existing.Id == area.Id));

        area.IsDeleted = true;
        await context.SaveChangesAsync();

        Assert.False(await context.AdministrativeAreas.AnyAsync(existing => existing.Id == area.Id));
        Assert.True(await repository.ExistsIncludingDeletedAsync("TN", "SF"));
        Assert.False(await repository.ExistsIncludingDeletedAsync("FR", "SF"));
    }
}
