namespace Studio.Application.Tests.Cities.Queries
{
    using Shouldly;
    using Studio.Application.Cities.Queries.GetAllNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllCitiesNames : QueryTestFixture
    {
        private GetCitiesNamesListQueryHandler sut;

        public GetAllCitiesNames()
        {
            QueryArrangeHelper.AddCities(context);
            sut = new GetCitiesNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetCitiesNamesTest()
        {
            var result = await sut.Handle(new GetCitiesNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<CitiesNamesListViewModel>();

            result.Cities.Count.ShouldBe(GConst.ValidQueryCount);
        }
    }
}
