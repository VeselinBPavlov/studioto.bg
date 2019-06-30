namespace Studio.Application.Countries.Commands.Update
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
    using Studio.Common;

    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateCountryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await this.context.Countries
                .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (country == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            bool isCountryUnique = context.Countries.Any(x => x.Name == request.Name);

            if (isCountryUnique)
            {
                throw new UpdateFailureException(nameof(Country), request.Name, string.Format(GlobalConstants.UniqueNameException, "country"));
            }
            
            country.Name = request.Name;
            country.ModifiedOn = DateTime.UtcNow;

            this.context.Countries.Update(country);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
