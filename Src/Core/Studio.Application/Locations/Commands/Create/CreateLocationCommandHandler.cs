namespace Studio.Application.Locations.Commands.Create
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Entities;
    using Domain.Enumerations;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

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

            var location = new Location
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

            this.context.Locations.Add(location);

            await this.context.SaveChangesAsync(cancellationToken);

            Directory.CreateDirectory($"../Studio.User.WebApp/wwwroot/img/locations/{location.Id}");

            return Unit.Value;
        }
    }
}
