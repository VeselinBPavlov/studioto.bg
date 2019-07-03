namespace Studio.Application.Employees.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Interfaces.Infrastructure;
    using Notifications;

    public class CreateEmployeeCommandNotification : INotification
    {
        public int EmployeeId { get; set; }

        public class EmployeeCreatedHandler : INotificationHandler<CreateEmployeeCommandNotification>
        {
            private readonly INotificationService notification;

            public EmployeeCreatedHandler(INotificationService notification)
            {
                this.notification = notification;
            }

            public async Task Handle(CreateEmployeeCommandNotification notification, CancellationToken cancellationToken)
            {
                await this.notification.SendAsync(new Message());
            }
        }
    }
}
