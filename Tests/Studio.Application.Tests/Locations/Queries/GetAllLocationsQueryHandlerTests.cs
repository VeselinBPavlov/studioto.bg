namespace Studio.Application.Tests.Locations.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Shouldly;
    using Studio.Application.Cities.Queries.GetAllCities;
    using Studio.Application.Locations.Queries.GetAllLocations;
    using Studio.Application.Locations.Queries.GetFilteredLocations;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Studio.Persistence.Context;
    using Xunit;

    [Collection("QueryCollection")]
    public class GetAllLocationsQueryHandlerTests : QueryTestFixture
    {
        private GetAllLocationsListQueryHandler sut;
        public GetAllLocationsQueryHandlerTests()
        {
            QueryArrangeHelper.AddLocations(context);
            sut = new GetAllLocationsListQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetAllLocationsTest()
        {
            var result = await sut.Handle(new GetAllLocationsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<LocationsListViewModel>();

            result.Locations.Count.ShouldBe(GConst.ValidCount);
        }
    }       
}