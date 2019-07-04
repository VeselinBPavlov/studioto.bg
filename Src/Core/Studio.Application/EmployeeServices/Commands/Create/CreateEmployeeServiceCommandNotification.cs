namespace Studio.Application.EmployeeServices.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateEmployeeServiceCommandNotification : INotification
    {
        public int EmployeeId { get; set; }
        public int ServiceId { get; set; }

        public class EmployeeServiceCreatedHandler : INotificationHandler<CreateEmployeeServiceCommandNotification>
        {
            private readonly INotificationService notification;

            public EmployeeServiceCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateEmployeeServiceCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
