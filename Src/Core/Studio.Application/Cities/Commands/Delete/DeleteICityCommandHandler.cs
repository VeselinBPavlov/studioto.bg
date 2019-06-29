namespace Studio.Application.Cities.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteCityCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = await context.Cities.FindAsync(request.Id);

            if (city == null)
            {
                throw new NotFoundException(nameof(City), request.Id);
            }

            if (city.IsDeleted)
            {
                throw new DeleteFailureException(nameof(City), request.Id, "City is already deleted.");
            }

            var hasAddresses = this.context.Addresses.Any(s => s.CityId == city.Id);

            if (hasAddresses)
            {
                throw new DeleteFailureException(nameof(City), request.Id, "There are existing address associated with this city.");
            }            

            city.DeletedOn = DateTime.UtcNow;
            city.IsDeleted = true;
            
            this.context.Cities.Update(city);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
