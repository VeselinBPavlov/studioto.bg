namespace Studio.Application.Services.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateServiceCommandNotification : INotification
    {
        public int ServiceId { get; set; }

        public class ServiceCreatedHandler : INotificationHandler<CreateServiceCommandNotification>
        {
            private readonly INotificationService notification;

            public ServiceCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateServiceCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
