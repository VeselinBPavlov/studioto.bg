namespace Studio.Application.Tests.Industries.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Industries.Queries.GetIndustryById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetIndustryByIdTests : QueryTestFixture
    {
        private GetIndustryByIdQueryHandler sut;

        public GetIndustryByIdTests()
        {
            QueryArrangeHelper.AddIndustries(context);
            sut = new GetIndustryByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetIndustryByIdTest()
        {
            var status = await sut.Handle(new GetIndustryByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<IndustryViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetIndustryByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Industry, GConst.InvalidId), status.Message);
        }
    }
}