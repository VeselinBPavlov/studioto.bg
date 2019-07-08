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
        private string userId;
        private int serviceId;
        private int locationId;
        private int employeeId;
        private Mock<IMediator> mediator;
        private CreateAppointmentCommandHandler sut;

        public CreateAppointmentCommandNotificationTests()
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
        public void ShouldRaiseAppointmentCreatedNotification()
        {
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

            mediator.Verify(m => m.Publish(It.Is<CreateAppointmentCommandNotification>(c => c.AppointmentId == appointmentId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}