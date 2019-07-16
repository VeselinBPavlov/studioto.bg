namespace Studio.Application.Countries.Commands.Delete
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

    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteCountryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await this.context.Countries
                .FindAsync(request.Id);

            if (country == null || country.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Country, request.Id);
            }

            var hasCities = this.context.Cities.Any(c => c.CountryId == country.Id && c.Country.IsDeleted == false);

            if (hasCities)
            {
                throw new DeleteFailureException(GConst.Country, request.Id, string.Format(GConst.DeleteException, GConst.Cities, GConst.CountryLower));
            }

            country.DeletedOn = DateTime.UtcNow;
            country.IsDeleted = true;
            
            this.context.Countries.Update(country);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
