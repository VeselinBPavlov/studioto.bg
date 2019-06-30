namespace Studio.Application.Cities.Commands.Update
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

    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateCityCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var city = await this.context.Cities
                .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (city == null)
            {
                throw new NotFoundException(GConst.City, request.Id);
            }

            var country = await this.context.Countries.FindAsync(request.CountryId);

            if (country == null)
            {
                throw new UpdateFailureException(GConst.City, request.Id, string.Format(GConst.RefereceException, GConst.CountryLower, request.CountryId));
            }

            city.Name = request.Name;
            city.CountryId = request.CountryId;
            city.ModifiedOn = DateTime.UtcNow;

            this.context.Cities.Update(city);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
