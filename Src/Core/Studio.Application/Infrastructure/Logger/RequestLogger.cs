namespace Studio.Application.Infrastructure.Logger
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using MediatR.Pipeline;

    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            
            _logger.LogInformation("Studio Request: {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }
}
