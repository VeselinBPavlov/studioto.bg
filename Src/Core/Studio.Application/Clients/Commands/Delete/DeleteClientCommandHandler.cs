﻿namespace Studio.Application.Industries.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
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

            var hasLocations = this.context.Locations.Any(s => s.ClientId == client.Id);

            if (hasLocations)
            {
                throw new DeleteFailureException(nameof(Location), request.Id, "There are existing locations associated with this client.");
            }

            client.DeletedOn = DateTime.UtcNow;
            client.IsDeleted = true;

            await this.context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}