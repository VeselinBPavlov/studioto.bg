namespace Studio.Application.Countries.Commands.Create
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Entities;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

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
            bool isCountryNotUnique = this.context.Countries.Any(c => c.Name == request.Name);

            if (isCountryNotUnique)
            {
                throw new CreateFailureException(GConst.Country, request.Name, string.Format(GConst.UniqueNameException, GConst.CountryLower));
            }

            var country = new Country
            {
                Name = request.Name,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Countries.Add(country);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
