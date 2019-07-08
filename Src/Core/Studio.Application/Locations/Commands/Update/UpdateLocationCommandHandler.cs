namespace Studio.Application.Locations.Commands.Update
{
    using Studio.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Exceptions;
    using Interfaces.Persistence;
    using System;
    using System.Linq;
    using Studio.Common;
    using Studio.Domain.Enumerations;

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateLocationCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await this.context.Locations
                .SingleOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted != true, cancellationToken);

            if (location == null)
            {
                throw new NotFoundException(GConst.Location, request.Id);
            }

            var client = await this.context.Clients.FindAsync(request.ClientId);

            if (client == null || client.IsDeleted == true) 
            {
                throw new UpdateFailureException(GConst.Location, request.Id, string.Format(GConst.RefereceException, GConst.ClientLower, request.ClientId));
            }

            var address = await this.context.Addresses.FindAsync(request.AddressId);

            if (address == null || address.IsDeleted == true) 
            {
                throw new UpdateFailureException(GConst.Location, request.Id, string.Format(GConst.RefereceException, GConst.AddressLower, request.AddressId));
            }

            location.Name = request.Name;
            location.IsOffice = request.IsOffice;
            location.StartDay = Enum.Parse<Workday>(request.StartDay);
            location.EndDay = Enum.Parse<Workday>(request.EndDay);
            location.StartHour = request.StartHour;
            location.EndHour = request.EndHour;
            location.ClientId = request.ClientId;
            location.AddressId = request.AddressId;
            location.ModifiedOn = DateTime.UtcNow;

            this.context.Locations.Update(location);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
