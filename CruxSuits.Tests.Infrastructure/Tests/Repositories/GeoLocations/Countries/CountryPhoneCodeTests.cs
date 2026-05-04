using CruxIT.Library.Exceptions;
using CruxSuits.Infrastructure.RepositoryPatterns;
using CruxSuits.Tests.Infrastructure.Tests.Repositories.Permissions.Users;
using CruxSuits.Tests.Infrastructure.Utilities.EntityFramework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CruxSuits.Tests.Infrastructure.Tests.Repositories.GeoLocations.Countries
{
    public class CountryPhoneCodeTests : BaseTests
    {
        public CountryPhoneCodeTests(SharedCruxSuitsSqliteFixture fixture) : base(fixture)
        {
        }


        [Fact]
        public async Task GetPhoneCode_ShouldReturn_216_When_Country_tn()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                string phoneCode = await uow.Country.GetCountryPhoneCode("TN");
                phoneCode.Should().Be("+216");
            }
        }

        [Fact]
        public async Task GetPhoneCode_ShouldReturn_216_When_Country_tun()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                string phoneCode = await uow.Country.GetCountryPhoneCode("TUN");
                phoneCode.Should().Be("+216");
            }
        }

        [Fact]
        public async Task GetPhoneCode_Should_Throw_Exception_When_Country_NotFound()
        {
            using (CruxSuitsUnitOfWork uow = await Fixture.CreateUnitOfWorkAsync())
            {
                try
                {
                    string phoneCode = await uow.Country.GetCountryPhoneCode("abc");
                }
                catch (Exception ex)
                {
                    ex.Should().BeOfType<CxException>();
                }
            }
        }

    }
}
