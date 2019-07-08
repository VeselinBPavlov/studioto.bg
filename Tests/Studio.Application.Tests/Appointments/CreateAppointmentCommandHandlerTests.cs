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
        private string userId;
        private int serviceId;
        private int locationId;
        private int employeeId;
        private Mock<IMediator> mediator;
        private CreateAppointmentCommandHandler sut;

        public CreateAppointmentCommandHandlerTests()
        {
            this.locationId = ArrangeHelper.GetLocationId(context, null, null);
            this.userId = ArrangeHelper.GetUserId(context);
            this.serviceId = ArrangeHelper.GetServiceId(context, null);
            this.employeeId = ArrangeHelper.GetEmployeeId(context, locationId);
            ArrangeHelper.AddEmployeeService(context, serviceId, employeeId);
            this.mediator = new Mock<IMediator>();
            this.sut = new CreateAppointmentCommandHandler(context, this.mediator.Object);
        }

        [Fact]
        public async Task ShouldCreateAppointment()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None));
           
            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Appointments.Count());
        }

        [Fact]
        public async Task ShouldNotThrowCreateFailureExceptionForEmptyUser()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
                EmployeeId = employeeId,
                ServiceId = serviceId
            }, CancellationToken.None));

            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Appointments.Count());
        }

        [Fact]
        public async Task ShouldNotThrowCreateFailureExceptionAndFindUserByEmail()
        {
            var status = Task<Unit>.FromResult(await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                Email = GConst.ValidEmail,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
            }, CancellationToken.None));

            Assert.Null(status.Exception);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Appointments.Count());
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidServiceId()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand { FirstName = GConst.ValidName, EmployeeId = employeeId, UserId = userId ,ServiceId = GConst.InvalidId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Appointment, GConst.InvalidId, GConst.ServiceLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForInvalidEmployeeId()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand { FirstName = GConst.ValidName, EmployeeId = GConst.InvalidId, ServiceId = serviceId, UserId = userId }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Create, GConst.Appointment, GConst.InvalidId, GConst.EmployeeLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForNotFreeHours()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                Email = GConst.ValidEmail,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.AllHoursBusy,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.FailureException, GConst.Create, GConst.Appointment, GConst.ValidEmail, string.Format(GConst.NotAvalableHours, new DateTime(2019, 09, 09).ToShortDateString())), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForWorkHourBefore()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                Email = GConst.ValidEmail,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.InvalidHourBefore,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.FailureException, GConst.Create, GConst.Appointment, GConst.ValidEmail, string.Format(GConst.InvalidAppointmentHourException, GConst.ValidStartHour, GConst.ValidEndHour)), status.Message);
        }

        [Fact]
        public async Task ShouldThrowCreateFailureExceptionForWorkHourAfter()
        {
            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                Email = GConst.ValidEmail,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.InvalidHourAfter,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.FailureException, GConst.Create, GConst.Appointment, GConst.ValidEmail, string.Format(GConst.InvalidAppointmentHourException,  GConst.ValidStartHour, GConst.ValidEndHour)), status.Message);
        }

        [Fact]
         public async Task ShouldThrowCreateFailureExceptionForClashAppointments()
        {
            ArrangeHelper.GetAppointmentId(context, serviceId, employeeId, userId);

            var status = await Record.ExceptionAsync(async () => await sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                Email = GConst.ValidEmail,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.FailureException, GConst.Create, GConst.Appointment, GConst.ValidEmail, string.Format(GConst.ReservedHourException, GConst.ValidName, new DateTime(2019, 09, 09).ToShortDateString(), DateTime.Parse(GConst.ValidHour).ToShortTimeString())), status.Message);
        }
    }
}
