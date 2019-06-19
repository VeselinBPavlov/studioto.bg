namespace Studio.Application.Infrastructure.Logger
{
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch timer;
        private readonly ILogger<TRequest> logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            this.timer = new Stopwatch();

            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            this.timer.Start();

            var response = await next();

            this.timer.Stop();

            if (this.timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;

                this.logger.LogWarning("Studio Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", name, this.timer.ElapsedMilliseconds, request);
            }

            return response;
        }
    }
}
