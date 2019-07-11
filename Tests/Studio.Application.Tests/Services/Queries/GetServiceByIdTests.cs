namespace Studio.Application.Tests.Services.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Services.Queries.GetServiceById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetServiceByIdTests : QueryTestFixture
    {
        private GetServiceByIdQueryHandler sut;

        public GetServiceByIdTests()
        {
            QueryArrangeHelper.AddServices(context);
            sut = new GetServiceByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetServiceByIdTest()
        {
            var status = await sut.Handle(new GetServiceByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<ServiceViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetServiceByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Service, GConst.InvalidId), status.Message);
        }
    }
}