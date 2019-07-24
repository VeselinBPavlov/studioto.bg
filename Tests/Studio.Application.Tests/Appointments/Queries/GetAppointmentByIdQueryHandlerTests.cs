namespace Studio.Application.Tests.Appointments.Queries
{
    using Shouldly;
    using Studio.Application.Appointments.Queries.GetAppointmentById;
    using Studio.Application.Clients.Queries.GetClientById;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class GetAppointmentByIdQueryHandlerTests : QueryTestFixture
    {
        private GetAppointmentByIdQueryHandler sut;

        public GetAppointmentByIdQueryHandlerTests()
        {
            QueryArrangeHelper.AddAppointmentes(context);
            sut = new GetAppointmentByIdQueryHandler(context);
        }

        [Fact]
        public async Task GetAppointmentByIdTest()
        {
            var status = await sut.Handle(new GetAppointmentByIdQuery { Id = GConst.ValidId }, CancellationToken.None);

            status.ShouldBeOfType<AppointmentViewModel>();
            status.Id.ShouldBe(GConst.ValidId);
        }

        [Fact]
        public async Task ShouldThowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new GetAppointmentByIdQuery { Id = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Appointment, GConst.InvalidId), status.Message);
        }
    }
}