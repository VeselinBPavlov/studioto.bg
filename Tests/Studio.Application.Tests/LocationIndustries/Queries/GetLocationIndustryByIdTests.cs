namespace Studio.Application.Tests.LocationIndustries.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Studio.Application.Cities.Queries.GetLocationIndustryById;

    public class GetLocationIndustryByIdTests : QueryTestFixture
    {
        private GetLocationIndustryByIdQueryHandler sut;

        public GetLocationIndustryByIdTests()
        {
            QueryArrangeHelper.AddLocationIndustries(context);
            sut = new GetLocationIndustryByIdQueryHandler(context);
        }

        // [Fact]
        // public async Task GetLocationIndustryByIdTest()
        // {
        //     var status = await sut.Handle(new GetLocationIndustryByIdQuery { LocationId = GConst.ValidId, IndustryId = GConst.ValidId }, CancellationToken.None);

        //     status.ShouldBeOfType<LocationIndustryViewModel>();
        //     status.LocationId.ShouldBe(GConst.ValidId);
        //     status.IndustryId.ShouldBe(GConst.ValidId);
        // }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetLocationIndustryByIdQuery { LocationId = GConst.InvalidId, IndustryId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.LocationIndustry, GConst.InvalidId + "/" + GConst.InvalidId), status.Message);
        }
    }
}