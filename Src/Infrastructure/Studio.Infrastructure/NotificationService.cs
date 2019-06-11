namespace Studio.Infrastructure
{
    using System.Threading.Tasks;

    using Application.Interfaces.Infrastructure;
    using Application.Notifications;

    public class NotificationService : INotificationService
    {
        public Task SendAsync(Message message)
        {
            return Task.CompletedTask;
        }
    }
}
