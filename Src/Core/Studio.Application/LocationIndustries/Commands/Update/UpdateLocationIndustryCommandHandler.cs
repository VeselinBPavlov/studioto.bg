namespace Studio.Application.LocationIndustries.Commands.Update
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

    public class UpdateLocationIndustryCommandHandler : IRequestHandler<UpdateLocationIndustryCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateLocationIndustryCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateLocationIndustryCommand request, CancellationToken cancellationToken)
        {
            var locationIndustry = await this.context.LocationIndustries
                .SingleOrDefaultAsync(c => c.LocationId == request.LocationId && c.IndustryId == request.IndustryId && c.IsDeleted != true, cancellationToken);

            if (locationIndustry == null)
            {
                throw new NotFoundException(GConst.LocationIndustry, $"{request.LocationId} - {request.IndustryId}");
            }

            locationIndustry.IsActive = request.IsActive;
            locationIndustry.Description = request.Description;
            locationIndustry.ModifiedOn = DateTime.UtcNow;

            this.context.LocationIndustries.Update(locationIndustry);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
