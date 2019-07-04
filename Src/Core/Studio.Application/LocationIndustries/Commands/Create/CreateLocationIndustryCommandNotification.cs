namespace Studio.Application.LocationIndustries.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateLocationIndustryCommandNotification : INotification
    {
        public int LocationId { get; set; }
        public int IndustryId { get; set; }

        public class LocationIndustryCreatedHandler : INotificationHandler<CreateLocationIndustryCommandNotification>
        {
            private readonly INotificationService notification;

            public LocationIndustryCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateLocationIndustryCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
