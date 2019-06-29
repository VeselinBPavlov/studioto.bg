namespace Studio.Application.Addresses.Commands.Update
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
    using Studio.Domain.ValueObjects;

    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateAddressCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await this.context.Addresses
                .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (address == null)
            {
                throw new NotFoundException(nameof(Address), request.Id);
            }

            var city = await this.context.Cities.FindAsync(request.CityId);

            if (city == null)
            {
                throw new UpdateFailureException(nameof(Address), request.Street, $"There are no existing city with this id {request.CityId}.");
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

            address.AddressFormat = AddressFormat.For(inputAddressData);
            address.Longitude = request.Longitude;
            address.Latitude = request.Latitude;
            address.CityId = request.CityId;
            address.ModifiedOn = DateTime.UtcNow;

            this.context.Addresses.Update(address);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
