namespace Studio.Application.Cities.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateCityCommandNotification : INotification
    {
        public int CityId { get; set; }

        public class CityCreatedHandler : INotificationHandler<CreateCityCommandNotification>
        {
            private readonly INotificationService notification;

            public CityCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateCityCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
