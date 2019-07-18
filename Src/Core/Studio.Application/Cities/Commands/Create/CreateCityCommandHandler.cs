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
    using Studio.Common;

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
            var country = await this.context.Countries.FindAsync(request.CountryId);

            if (country == null || country.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.City, request.Name, string.Format(GConst.RefereceException, GConst.CountryLower, request.CountryId));
            }

            var city = new City
            {
                Name = request.Name,
                CountryId = request.CountryId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Cities.Add(city);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
