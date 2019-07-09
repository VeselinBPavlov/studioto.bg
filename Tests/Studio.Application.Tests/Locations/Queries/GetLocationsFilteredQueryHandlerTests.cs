namespace Studio.Application.Tests.Locations.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Cities.Queries.GetAllCities;
    using Studio.Application.Locations.Queries.GetFilteredLocations;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetLocationsFilteredQueryHandlerTests : QueryTestFixture
    {
        private GetFilteredLocationsListQueryHandler sut;
        public GetLocationsFilteredQueryHandlerTests()
        {
            QueryArrangeHelper.AddLocations(context);
            sut = new GetFilteredLocationsListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetLocationsWithoutFiltersTest()
        {
            var result = await sut.Handle(new GetFilteredLocationsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<LocationsFilteredListViewModel>();

            result.Locations.Count.ShouldBe(GConst.ValidCount);
        }

        [Fact]
        public async Task GetLocationsWithStudioNameFilterTest()
        {
            var result = await sut.Handle(new GetFilteredLocationsListQuery { StudioName = GConst.ValidName }, CancellationToken.None);

            result.ShouldBeOfType<LocationsFilteredListViewModel>();

            result.Locations.Count.ShouldBe(GConst.ValidCount);
        }

        // TODO: Add test for cityId filter and search field cases
    }       
}