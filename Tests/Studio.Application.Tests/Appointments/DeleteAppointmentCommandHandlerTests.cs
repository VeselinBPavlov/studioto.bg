namespace Studio.Application.Tests.Appointments.Commands
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Appointments.Commands.Delete;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class DeleteAppointmentCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteAppointment()
        {
            var appointmentId = GetAppointmentId(null, null, null);

            var sut = new DeleteAppointmentCommandHandler(context);

            var status = Task<Unit>.FromResult(await sut.Handle(new DeleteAppointmentCommand { Id = appointmentId }, CancellationToken.None));
                        
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
        }


        [Fact]
        public async Task AppointmentShouldТhrowNotFoundException()
        {
            var sut = new DeleteAppointmentCommandHandler(context);           

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new DeleteAppointmentCommand { Id = GConst.InvalidId }, CancellationToken.None));
           
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Appointment, GConst.InvalidId), status.Message);
        }
    }
}
