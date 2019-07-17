namespace Studio.Application.Tests.Appointments.Commands
{
    using MediatR;
    using Studio.Application.Appointments.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateAppointmentCommandHandlerTests : CommandTestBase
    {
        private int locationId;
        private int serviceId;
        private int employeeId;
        private int appointmentId;
        private UpdateAppointmentCommandHandler sut;

        public UpdateAppointmentCommandHandlerTests()
        {
            this.locationId = CommandArrangeHelper.GetLocationId(context, null, null);
            this.serviceId = CommandArrangeHelper.GetServiceId(context, null);
            this.employeeId = CommandArrangeHelper.GetEmployeeId(context, locationId);
            CommandArrangeHelper.AddEmployeeService(context, serviceId, employeeId);
            this.appointmentId = CommandArrangeHelper.GetAppointmentId(context, serviceId, employeeId, null);
            this.sut = new UpdateAppointmentCommandHandler(context);
        }

        [Fact]
        public async void AppointmentShouldUpdateCorrect()
        {
            var updatedAppointment = new UpdateAppointmentCommand
            {
                Id = appointmentId,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
                EmployeeId = employeeId,
                ServiceId = serviceId
            };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedAppointment, CancellationToken.None));

            var resultId = context.Appointments.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            Assert.Equal(appointmentId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Appointments.Count());
        }

        [Fact]
        public async void AppointmentShouldThrowReferenceExceptionForInvalidServiceId()
        {
            var updatedAppointment = new UpdateAppointmentCommand { Id = appointmentId, ServiceId = GConst.InvalidId, EmployeeId = employeeId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Appointment, appointmentId, GConst.ServiceLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void AppointmentShouldThrowReferenceExceptionForInvalidEmployeeId()
        {
            var updatedAppointment = new UpdateAppointmentCommand { Id = appointmentId, ServiceId = serviceId, EmployeeId = GConst.InvalidId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Appointment, appointmentId, GConst.EmployeeLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void AppointmentShouldThrowNotFoundException()
        {
            var updatedAppointment = new UpdateAppointmentCommand { Id = GConst.InvalidId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Appointment, GConst.InvalidId), status.Message);            
        }

        [Fact]
        public async void AppointmentShouldThrowUpdateExceptionBeforeWorkingHour()
        {
            var updatedAppointment = new UpdateAppointmentCommand
            {
                Id = appointmentId,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.InvalidHourBefore,
                EmployeeId = employeeId,
                ServiceId = serviceId
            };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.FailureException, GConst.Update, GConst.Appointment, GConst.InvalidHourBefore, string.Format(GConst.InvalidAppointmentHourException, GConst.ValidStartHour, GConst.ValidEndHour)), status.Message);
        }

        [Fact]
        public async void AppointmentShouldThrowUpdateExceptionAfterWorkingHour()
        {
            var updatedAppointment = new UpdateAppointmentCommand
            {
                Id = appointmentId,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.InvalidHourAfter,
                EmployeeId = employeeId,
                ServiceId = serviceId
            };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.FailureException, GConst.Update, GConst.Appointment, GConst.InvalidHourAfter, string.Format(GConst.InvalidAppointmentHourException, GConst.ValidStartHour, GConst.ValidEndHour)), status.Message);
        }
    }
}
