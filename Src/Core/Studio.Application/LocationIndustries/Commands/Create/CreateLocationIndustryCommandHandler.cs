namespace Studio.Application.LocationIndustries.Commands.Create
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

    public class CreateLocationIndustryCommandHandler : IRequestHandler<CreateLocationIndustryCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateLocationIndustryCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateLocationIndustryCommand request, CancellationToken cancellationToken)
        {
            var location = await this.context.Locations.FindAsync(request.LocationId);

            if (location == null || location.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.LocationIndustry, request.LocationId, string.Format(GConst.RefereceException, GConst.LocationLower, request.LocationId));
            }

            var industry = await this.context.Industries.FindAsync(request.IndustryId);

            if (industry == null || industry.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.LocationIndustry, request.IndustryId, string.Format(GConst.RefereceException, GConst.IndustryLower, request.IndustryId));
            }

            bool isActive = location.Employees.Any(x => x.EmployeeServices.Any(y => y.Service.IndustryId == industry.Id));

            var locationIndustry = new LocationIndustry
            {
                LocationId = location.Id,
                Location = location,
                IndustryId = industry.Id,
                Industry = industry,
                Description = request.Description,
                IsActive = isActive,
                CreatedOn = DateTime.UtcNow
            };

            this.context.LocationIndustries.Add(locationIndustry);

            await this.context.SaveChangesAsync(cancellationToken);            

            return Unit.Value;
        }
    }
}
