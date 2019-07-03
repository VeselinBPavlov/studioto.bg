namespace Studio.Application.Appointments.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateAppointmentCommandNotification : INotification
    {
        public int AppointmentId { get; set; }

        public class AppointmentCreatedHandler : INotificationHandler<CreateAppointmentCommandNotification>
        {
            private readonly INotificationService notification;

            public AppointmentCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateAppointmentCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
