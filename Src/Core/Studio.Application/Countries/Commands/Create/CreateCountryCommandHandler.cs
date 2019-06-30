namespace Studio.Application.Countries.Commands.Create
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

    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateCountryCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            bool isCountryUnique = context.Countries.Any(x => x.Name == request.Name);

            if (isCountryUnique)
            {
                throw new CreateFailureException(nameof(Country), request.Name, string.Format(GlobalConstants.UniqueNameException, "country"));
            }

            var country = new Country
            {
                Name = request.Name,
                CreatedOn = DateTime.UtcNow
            };

            context.Countries.Add(country);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateCountryCommandNotification { CountryId = country.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
