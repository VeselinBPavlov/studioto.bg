﻿namespace Studio.Application.Industries.Commands.Update
{
    using Studio.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Exceptions;
    using Interfaces.Persistence;
    using System;
    using Studio.Domain.ValueObjects;

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
                .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
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