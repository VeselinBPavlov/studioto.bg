namespace Studio.Application.Clients.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteClientCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients
                .FindAsync(request.Id);

            if (client == null || client.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Client, request.Id);
            }

            var hasLocations = this.context.Locations.Where(l => l.IsDeleted != true).Any(l => l.ClientId == client.Id && l.Client.IsDeleted == false);

            if (hasLocations)
            {
                throw new DeleteFailureException(GConst.Client, request.Id, string.Format(GConst.DeleteException, GConst.Locations, GConst.ClientLower));
            }

            client.DeletedOn = DateTime.UtcNow;
            client.IsDeleted = true;

            this.context.Clients.Update(client);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
