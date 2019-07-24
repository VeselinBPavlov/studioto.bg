namespace Studio.Application.Cities.Queries.GetLocationIndustryById
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetLocationIndustryByIdQueryHandler : IRequestHandler<GetLocationIndustryByIdQuery, LocationIndustryViewModel>
    {
        private readonly IStudioDbContext context;

        public GetLocationIndustryByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<LocationIndustryViewModel> Handle(GetLocationIndustryByIdQuery request, CancellationToken cancellationToken)
        {
            var locationIndustry = await this.context.LocationIndustries
                .Include(c => c.Location)
                .Include(c => c.Industry)
                .SingleOrDefaultAsync(c => c.LocationId == request.LocationId && c.IndustryId == request.IndustryId);

            if (locationIndustry == null)
            {
                throw new NotFoundException(GConst.LocationIndustry, request.LocationId + "/" + request.IndustryId);
            }

            return LocationIndustryViewModel.Create(locationIndustry);
        }
    }
}