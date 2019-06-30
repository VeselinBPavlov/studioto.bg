namespace Studio.Application.Addresses.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteAddressCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await context.Addresses.FindAsync(request.Id);

            if (address == null)
            {
                throw new NotFoundException(nameof(Address), request.Id);
            }

            var hasLocation = this.context.Locations.Any(s => s.AddressId == address.Id);

            if (hasLocation)
            {
                throw new DeleteFailureException(nameof(Address), request.Id, string.Format(GlobalConstants.DeleteException, "location", "address"));
            }            

            address.DeletedOn = DateTime.UtcNow;
            address.IsDeleted = true;
            
            this.context.Addresses.Update(address);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
