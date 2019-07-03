namespace Studio.Application.Addresses.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System.Linq;
    using Studio.Application.Exceptions;
    using System;
    using Studio.Domain.ValueObjects;
    using Studio.Common;

    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateAddressCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var city = await this.context.Cities.FindAsync(request.CityId);

            if (city == null || city.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Address, request.Street, string.Format(GConst.RefereceException, GConst.CityLower, request.CityId));
            }

            var inputAddressData = new InputAddressData
            {
                Street = request.Street,
                Number = request.Number,
                Building = request.Building,
                Entrance = request.Entrance,
                Floor = request.Floor,
                Apartment = request.Apartment,
                District = request.District
            };

            var address = new Address
            {
                AddressFormat = AddressFormat.For(inputAddressData),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                CityId = request.CityId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Addresses.Add(address);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateAddressCommandNotification { AddressId = address.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
