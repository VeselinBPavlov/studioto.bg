namespace Studio.Application.Clients.Commands.Update
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.ValueObjects;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateClientCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients
                .SingleOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted != true, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(GConst.Client, request.Id);
            }

            var manager = Manager.For($"{request.ManagerFirstName} {request.ManagerLastName}");

            client.CompanyName = request.CompanyName;
            client.VatNumber = request.VatNumber;
            client.Phone = request.Phone;
            client.Manager = manager;
            client.ModifiedOn = DateTime.UtcNow;

            this.context.Clients.Update(client);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
