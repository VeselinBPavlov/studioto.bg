namespace Studio.Application.Locations.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System.Linq;
    using Studio.Application.Exceptions;
    using System;
    using Studio.Common;
    using Studio.Domain.Enumerations;

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateLocationCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var client = await this.context.Clients.FindAsync(request.ClientId);

            if (client == null || client.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Location, request.Name, string.Format(GConst.RefereceException, GConst.ClientLower, request.ClientId));
            }

            var address = await this.context.Addresses.FindAsync(request.AddressId);

            if (address == null || address.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Location, request.Name, string.Format(GConst.RefereceException, GConst.AddressLower, request.AddressId));
            }

            var Location = new Location
            {
                Name = request.Name,
                IsOffice = request.IsOffice,
                Phone = request.Phone,
                Slogan = request.Slogan,
                Description = request.Description,
                StartDay = Enum.Parse<Workday>(request.StartDay),
                EndDay = Enum.Parse<Workday>(request.EndDay),
                StartHour = request.StartHour,
                EndHour = request.EndHour,
                ClientId = request.ClientId,
                AddressId = request.AddressId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Locations.Add(Location);

            await this.context.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new CreateLocationCommandNotification { LocationId = Location.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
