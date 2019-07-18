namespace Studio.Application.ContactForms.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateContactFormCommandNotification : INotification
    {
        public int ContactFormId { get; set; }

        public class ContactFormCreatedHandler : INotificationHandler<CreateContactFormCommandNotification>
        {
            private readonly INotificationService notification;

            public ContactFormCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateContactFormCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
