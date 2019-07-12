namespace Studio.Application.Locations.Queries.GetLocationById
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

    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationViewModel>
    {
        private readonly IStudioDbContext context;

        public GetLocationByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<LocationViewModel> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await context.Locations
                .Include(c => c.Address)
                    .ThenInclude(a => a.City)
                .Include(l => l.Client)
                .SingleOrDefaultAsync(c => c.Id == request.Id);

            if (location == null)
            {
                throw new NotFoundException(GConst.Location, request.Id);
            }

            return LocationViewModel.Create(location);
        }
    }
}