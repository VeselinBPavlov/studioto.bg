namespace Studio.Application.Cities.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System.Linq;
    using Studio.Application.Exceptions;
    using System;

    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateCityCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            bool isCountryExists = this.context.Countries.Any(c => c.Id == request.CountryId);

            if (isCountryExists == false) 
            {
                throw new CreateFailureException(nameof(City), request.Name, $"There are no existing country with id {request.CountryId}.");
            }

            var city = new City
            {
                Name = request.Name,
                CountryId = request.CountryId,
                CreatedOn = DateTime.UtcNow
            };

            context.Cities.Add(city);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateCityCommandNotification { CityId = city.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
