﻿namespace Studio.Application.Countries.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
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
            var country = await context.Countries
                .FindAsync(request.Id);

            if (country == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            var hasCities = this.context.Cities.Any(c => c.CountryId == country.Id);

            if (hasCities)
            {
                throw new DeleteFailureException(nameof(Country), request.Id, "There are existing cities associated with this country.");
            }

            country.DeletedOn = DateTime.UtcNow;
            country.IsDeleted = true;
            
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}