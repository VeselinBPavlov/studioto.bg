namespace Studio.Application.Clients.Commands.Delete
{
    using MediatR;
    using Studio.Application.Clients.Commands.Delete;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteClientCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await context.Clients
                .FindAsync(request.Id);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            var hasLocations = context.Locations.Any(s => s.ClientId == client.Id);

            if (hasLocations)
            {
                throw new DeleteFailureException(nameof(Client), request.Id, string.Format(GlobalConstants.DeleteException, "locations", "client"));
            }

            client.DeletedOn = DateTime.UtcNow;
            client.IsDeleted = true;

            context.Clients.Update(client);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
