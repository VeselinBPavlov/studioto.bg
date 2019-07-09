namespace Studio.Application.Tests.Cities
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Cities.Queries.GetAllCities;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllCitiesQueryHandlerTests : QueryTestFixture
    {
        private GetAllCitiesListQueryHandler sut;
        public GetAllCitiesQueryHandlerTests()
        {
            QueryArrangeHelper.AddCities(context);
            sut = new GetAllCitiesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetCitiesTest()
        {
            var result = await sut.Handle(new GetAllCitiesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<CitiesListViewModel>();

            result.Cities.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }       
}