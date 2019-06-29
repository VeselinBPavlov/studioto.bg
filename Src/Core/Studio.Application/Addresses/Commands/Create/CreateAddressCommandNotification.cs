namespace Studio.Application.Addresses.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateAddressCommandNotification : INotification
    {
        public int AddressId { get; set; }

        public class AddressCreatedHandler : INotificationHandler<CreateAddressCommandNotification>
        {
            private readonly INotificationService notification;

            public AddressCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateAddressCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
