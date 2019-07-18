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
