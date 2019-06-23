namespace Studio.Application.Industries.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateClientCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                Name = request.Name
            };

            context.Clients.Add(client);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateClientCommandNotification { ClientId = client.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
