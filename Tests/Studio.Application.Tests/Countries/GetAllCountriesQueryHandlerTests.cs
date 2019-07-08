namespace Studio.Application.Tests.Countries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Countries.Queries.GetAllCountries;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllCountriesQueryHandlerTests : QueryTestFixture
    {
        private GetAllCountriesListQueryHandler sut;
        public GetAllCountriesQueryHandlerTests()
        {
            AddCountries();
            sut = new GetAllCountriesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetCountriesTest()
        {
            var result = await sut.Handle(new GetAllCountriesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<CountriesListViewModel>();

            result.Countries.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}