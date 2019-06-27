namespace Studio.Application.Countries.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateCountryCommandNotification : INotification
    {
        public int CountryId { get; set; }

        public class CountryCreatedHandler : INotificationHandler<CreateCountryCommandNotification>
        {
            private readonly INotificationService notification;

            public CountryCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateCountryCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
