namespace Studio.Application.Interfaces.Infrastructure
{
    using System.Threading.Tasks;

    using Notifications;

    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
