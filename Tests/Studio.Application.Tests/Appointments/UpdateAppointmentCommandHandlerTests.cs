﻿namespace Studio.Application.Tests.Appointments.Commands
{
    using MediatR;
    using Studio.Application.Appointments.Commands.Update;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class UpdateAppointmentCommandHandlerTests : CommandTestBase
    {   
        [Fact]
        public async void AppointmentShouldUpdateCorrect()
        {
            var serviceId = GetServiceId(null);
            var employeeId = GetEmployeeId(null);

            var appointmentId = GetAppointmentId(serviceId, employeeId, null);

            var sut = new UpdateAppointmentCommandHandler(context);
            var updatedAppointment = new UpdateAppointmentCommand { Id = appointmentId, FirstName = GConst.ValidName, ServiceId = serviceId, EmployeeId = employeeId };

            var status = Task<Unit>.FromResult(await sut.Handle(updatedAppointment, CancellationToken.None));

            var resultId = context.Appointments.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            Assert.Equal(appointmentId, resultId);
            Assert.Equal(GConst.SuccessStatus, status.Status.ToString());
            Assert.Equal(GConst.ValidCount, context.Appointments.Count());
        }

        [Fact]
        public async void AppointmentShouldThrowReferenceExceptionForInvalidServiceId()
        {
           var serviceId = GetServiceId(null);
           var employeeId = GetEmployeeId(null);

            var appointmentId = GetAppointmentId(serviceId, employeeId, null);

            var sut = new UpdateAppointmentCommandHandler(context);
            var updatedAppointment = new UpdateAppointmentCommand { Id = appointmentId, FirstName = GConst.ValidName, ServiceId = GConst.InvalidId, EmployeeId = employeeId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Appointment, appointmentId, GConst.ServiceLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void AppointmentShouldThrowReferenceExceptionForInvalidEmployeeId()
        {
           var serviceId = GetServiceId(null);
           var employeeId = GetEmployeeId(null);

            var appointmentId = GetAppointmentId(serviceId, employeeId, null);

            var sut = new UpdateAppointmentCommandHandler(context);
            var updatedAppointment = new UpdateAppointmentCommand { Id = appointmentId, FirstName = GConst.ValidName, ServiceId = serviceId, EmployeeId = GConst.InvalidId };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));

            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.ReferenceExceptionMessage, GConst.Update, GConst.Appointment, appointmentId, GConst.EmployeeLower, GConst.InvalidId), status.Message);
        }

        [Fact]
        public async void AppointmentShouldThrowNotFoundException()
        {
            var sut = new UpdateAppointmentCommandHandler(context);
            var updatedAppointment = new UpdateAppointmentCommand { Id = GConst.InvalidId, FirstName = GConst.ValidName };

            var status = await Record.ExceptionAsync(async () => await sut.Handle(updatedAppointment, CancellationToken.None));
                        
            Assert.NotNull(status);
            Assert.Equal(string.Format(GConst.NotFoundExceptionMessage, GConst.Appointment, GConst.InvalidId), status.Message);            
        }
    }
}