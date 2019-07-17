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
            this.locationId = CommandArrangeHelper.GetLocationId(context, null, null);
            this.userId = CommandArrangeHelper.GetUserId(context);
            this.serviceId = CommandArrangeHelper.GetServiceId(context, null);
            this.employeeId = CommandArrangeHelper.GetEmployeeId(context, locationId);
            CommandArrangeHelper.AddEmployeeService(context, serviceId, employeeId);
            this.mediator = new Mock<IMediator>();
            this.sut = new CreateAppointmentCommandHandler(context, this.mediator.Object);
        }

        [Fact]
        public void ShouldRaiseAppointmentCreatedNotification()
        {
            var result = sut.Handle(new CreateAppointmentCommand
            {
                ReservationDate = new DateTime(2019, 09, 09),
                TimeBlockHelper = GConst.ValidHour,
                EmployeeId = employeeId,
                ServiceId = serviceId,
                UserId = userId
            }, CancellationToken.None);

            var appointmentId = context.Appointments.SingleOrDefault(x => x.TimeBlockHelper == GConst.ValidHour).Id;

            mediator.Verify(m => m.Publish(It.Is<CreateAppointmentCommandNotification>(c => c.AppointmentId == appointmentId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }    
}