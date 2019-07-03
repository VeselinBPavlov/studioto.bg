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
                .SingleOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted != true, cancellationToken);

            if (country == null)
            {
                throw new NotFoundException(GConst.Country, request.Id);
            }

            bool isCountryUnique = context.Countries.Any(c => c.Name == request.Name && c.IsDeleted == false);

            if (isCountryUnique)
            {
                throw new UpdateFailureException(GConst.Country, request.Name, string.Format(GConst.UniqueNameException, GConst.CountryLower));
            }
            
            country.Name = request.Name;
            country.ModifiedOn = DateTime.UtcNow;

            this.context.Countries.Update(country);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
