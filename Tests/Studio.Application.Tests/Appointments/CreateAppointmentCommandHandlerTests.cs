namespace Studio.Application.Tests.Appointments.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Studio.Application.Appointments.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Xunit;
    using Moq;
    using Studio.Domain.Entities;

    public class CreateAppointmentCommandHandlerTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateAppointment()
        {
            var userId = GetUserId();

            var serviceId = GetServiceId(null);

            var employeeId = GetEmployeeId(null);
            
            var mediator = new Mock<IMediator>();
            var sut = new CreateAppointmentCommandHandler(context, mediator.Object);

            var status = Task<Unit>.FromResult(await sut.Handle(new CreateAppointmentCommand { FirstName = GConst.ValidName, EmployeeId = employeeId, ServiceId = serviceId, UserId = userId }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Appointments.Count());
        }


        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidServiceId()
        {
            var userId = GetUserId();
            var employeeId = GetEmployeeId(null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateAppointmentCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand { FirstName = GConst.ValidName, EmployeeId = employeeId, UserId = userId ,ServiceId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Appointment, GConst.InvalidId, GConst.ServiceLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidEmployeeId()
        {
            var userId = GetUserId();
            var serviceId = GetServiceId(null);

            var mediator = new Mock<IMediator>();
            var sut = new CreateAppointmentCommandHandler(context, mediator.Object);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand { FirstName = GConst.ValidName, EmployeeId = GConst.InvalidId, ServiceId = serviceId, UserId = userId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Appointment, GConst.InvalidId, GConst.EmployeeLower, GConst.InvalidId), status.Message);
        }
    }
}
