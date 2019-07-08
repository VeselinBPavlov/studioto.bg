namespace Studio.Application.Locations.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateLocationCommandNotification : INotification
    {
        public int LocationId { get; set; }

        public class LocationCreatedHandler : INotificationHandler<CreateLocationCommandNotification>
        {
            private readonly INotificationService notification;

            public LocationCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateLocationCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
