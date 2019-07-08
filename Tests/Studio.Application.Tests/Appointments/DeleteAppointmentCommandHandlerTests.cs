namespace Studio.Application.Tests.Appointments.Commands
{
    using MediatR;
    using Studio.Application.Appointments.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class DeleteAppointmentCommandHandlerTests : CommandTestBase
    {
        private int appointmentId;
        private DeleteAppointmentCommandHandler sut;

        public DeleteAppointmentCommandHandlerTests()
        {
            appointmentId = ArrangeHelper.GetAppointmentId(context, null, null, null);
            sut = new DeleteAppointmentCommandHandler(context);
        }

        [Fact]
        public async Task ShouldDeleteAppointment()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteAppointmentCommand { Id = appointmentId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }


        [Fact]
        public async Task AppointmentShouldТhrowNotFoundException()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAppointmentCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Appointment, GConst.InvalidId), status.Message);
        }
    }
}
