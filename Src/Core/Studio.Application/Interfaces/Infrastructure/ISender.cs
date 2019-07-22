namespace Studio.Application.Interfaces.Infrastructure
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.Logging;

    public interface ISender
    {
        void ConfigureSendGridEmailSender(ILoggerFactory loggerFactory, string apiKey, string fromAddress, string fromName);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
