namespace Studio.Application.Tests.Appointments.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using MediatR;
    using Moq;
    using Studio.Application.Appointments.Commands.Create;
    using Studio.Application.Tests.Infrastructure;
    using Studio.Common;
    using Studio.Domain.Entities;
    using Xunit;

    public class CreateAppointmentCommandNotificationTests : CommandTestBase
    {
        [Fact]
        public void ShouldRaiseAppointmentCreatedNotification()
        {
            AddAdministration();

            var userId = GetUserId();

            var serviceId = GetServiceId(null);

            var employeeId = GetEmployeeId(null);

            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateAppointmentCommandHandler(context, mediatorMock.Object);

            var result = sut.Handle(new CreateAppointmentCommand
            {
                FirstName = GConst.ValidName,
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None);

            var appointmentId = context.Appointments.SingleOrDefault(x => x.FirstName == GConst.ValidName).Id;

            mediatorMock.Verify(m => m.Publish(It.Is<CreateAppointmentCommandNotification>(c => c.AppointmentId == appointmentId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}