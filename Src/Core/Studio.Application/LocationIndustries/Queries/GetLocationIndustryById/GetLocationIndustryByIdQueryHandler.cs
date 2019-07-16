namespace Studio.Application.Cities.Queries.GetLocationIndustryById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetLocationIndustryByIdQueryHandler : IRequestHandler<GetLocationIndustryByIdQuery, LocationIndustryViewModel>
    {
        private readonly IStudioDbContext context;

        public GetLocationIndustryByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<LocationIndustryViewModel> Handle(GetLocationIndustryByIdQuery request, CancellationToken cancellationToken)
        {
            var LocationIndustry = await this.context.LocationIndustries
                .Include(c => c.Location)
                .Include(c => c.Industry)
                .SingleOrDefaultAsync(c => c.LocationId == request.LocationId && c.IndustryId == request.IndustryId);

            if (LocationIndustry == null)
            {
                throw new NotFoundException(GConst.LocationIndustry, request.LocationId + "/" + request.IndustryId);
            }

            return LocationIndustryViewModel.Create(LocationIndustry);
        }
    }
}