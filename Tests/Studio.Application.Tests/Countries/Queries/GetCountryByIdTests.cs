namespace Studio.Application.Tests.Countries.Queries
{
    using Shouldly;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetCountryByIdTests : QueryTestFixture
    {
        private GetCountryByIdQueryHandler sut;

        public GetCountryByIdTests()
        {
            QueryArrangeHelper.AddCountries(context);
            sut = new GetCountryByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetCountryByIdTest()
        {
            var status = await sut.Handle(new GetCountryByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<CountryViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetCountryByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Country, GConst.InvalidId), status.Message);
        }
    }
}