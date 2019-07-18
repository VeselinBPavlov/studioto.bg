namespace Studio.Application.Industries.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateIndustryCommandNotification : INotification
    {
        public int IndustryId { get; set; }

        public class IndustryCreatedHandler : INotificationHandler<CreateIndustryCommandNotification>
        {
            private readonly INotificationService notification;

            public IndustryCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateIndustryCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
