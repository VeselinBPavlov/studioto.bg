﻿namespace Studio.Application.Cities.Commands.Update
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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
                .SingleOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted != true, cancellationToken);

            if (city == null)
            {
                throw new NotFoundException(GConst.City, request.Id);
            }

            var country = await this.context.Countries.FindAsync(request.CountryId);

            if (country == null || country.IsDeleted == true)
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
