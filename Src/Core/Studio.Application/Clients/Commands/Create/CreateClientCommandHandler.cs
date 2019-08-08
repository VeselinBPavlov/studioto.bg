namespace Studio.Application.Clients.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Domain.ValueObjects;
    using Interfaces.Persistence;
    using MediatR;

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

            return Unit.Value;
        }
    }
}
