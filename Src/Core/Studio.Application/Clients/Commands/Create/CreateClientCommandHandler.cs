namespace Studio.Application.Clients.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System;
    using Studio.Domain.ValueObjects;

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
            var manager = Manager.For($"{request.ManagerFirstName} {request.ManagerLastName}");

            var client = new Client
            {
                CompanyName = request.CompanyName,
                VatNumber = request.VatNumber,
                Phone = request.Phone,
                Manager = manager,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Clients.Add(client);

            await this.context.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new CreateClientCommandNotification { ClientId = client.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
