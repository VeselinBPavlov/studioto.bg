namespace Studio.Application.Tests.Cities.Queries
{
    using Shouldly;
    using Studio.Application.Cities.Queries.GetCityById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetCityByIdTests : QueryTestFixture
    {
        private GetCityByIdQueryHandler sut;

        public GetCityByIdTests()
        {
            QueryArrangeHelper.AddCities(context);
            sut = new GetCityByIdQueryHandler(context, mapper);
        }

        [Fact]
        public async Task GetCityByIdTest()
        {
            var status = await sut.Handle(new GetCityByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<CityViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetCityByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.City, GConst.InvalidId), status.Message);
        }
    }
}