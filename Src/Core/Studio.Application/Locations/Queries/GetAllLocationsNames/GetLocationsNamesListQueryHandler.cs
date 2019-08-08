namespace Studio.Application.Locations.Queries.GetAllLocationsNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetLocationsNamesListQueryHandler : IRequestHandler<GetLocationsNamesListQuery, LocationsNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetLocationsNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LocationsNamesListViewModel> Handle(GetLocationsNamesListQuery request, CancellationToken cancellationToken)
        {
            return new LocationsNamesListViewModel
            {
                Locations = await this.context.Locations.Where(c => c.IsDeleted != true && c.IsOffice == false).OrderBy(l => l.Client.CompanyName).ProjectTo<LocationNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
