namespace Studio.Application.Locations.Queries.GetAllLocations
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetAllLocationsListQueryHandler : IRequestHandler<GetAllLocationsListQuery, LocationsListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllLocationsListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LocationsListViewModel> Handle(GetAllLocationsListQuery request, CancellationToken cancellationToken)
        {
            return new LocationsListViewModel
            {
                Locations = await this.context.Locations.OrderByDescending(x => x.CreatedOn).ProjectTo<LocationAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}