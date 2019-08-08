namespace Studio.Application.Tests.Locations.Queries
{
    using Shouldly;
    using Studio.Application.Locations.Queries.GetAllLocationsNames;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllLocationsNames : QueryTestFixture
    {
        private readonly GetLocationsNamesListQueryHandler sut;

        public GetAllLocationsNames()
        {
            QueryArrangeHelper.AddLocations(context);
            sut = new GetLocationsNamesListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetLocationsNamesTest()
        {
            var result = await sut.Handle(new GetLocationsNamesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<LocationsNamesListViewModel>();

            result.Locations.Count.ShouldBe(GConst.ValidCount);
        }
    }
}
