namespace Studio.Application.Tests.Locations.Queries
{
    using Shouldly;
    using Studio.Application.Locations.Queries.GetLocationById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetLocationByIdTests : QueryTestFixture
    {
        private GetLocationByIdQueryHandler sut;

        public GetLocationByIdTests()
        {
            QueryArrangeHelper.AddLocations(context);
            sut = new GetLocationByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetLocationByIdTest()
        {
            var status = await sut.Handle(new GetLocationByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<LocationViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetLocationByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Location, GConst.InvalidId), status.Message);
        }
    }
}