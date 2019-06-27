namespace Studio.Application.Clients.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateClientCommandNotification : INotification
    {
        public int ClientId { get; set; }

        public class ClientCreatedHandler : INotificationHandler<CreateClientCommandNotification>
        {
            private readonly INotificationService notification;

            public ClientCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateClientCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
