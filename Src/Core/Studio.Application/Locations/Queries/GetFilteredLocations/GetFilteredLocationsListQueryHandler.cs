namespace Studio.Application.Locations.Queries.GetFilteredLocations
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

    public class GetFilteredLocationsListQueryHandler : IRequestHandler<GetFilteredLocationsListQuery, LocationsFilteredListViewModel>
    {
        private const int MaxLocationsCount = 8;
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetFilteredLocationsListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LocationsFilteredListViewModel> Handle(GetFilteredLocationsListQuery request, CancellationToken cancellationToken)
        {
            var locations = this.context.Locations.Where(l => l.IsDeleted != true).OrderByDescending(l => l.CreatedOn).AsQueryable();

            if (request.CityId != 0 && request.CityId != null) 
            {
                locations = locations.Where(l => l.Address.CityId == request.CityId.Value);
            }

            if (string.IsNullOrEmpty(request.StudioName) == false) 
            {
                locations = locations.Where(l => l.Name.ToLower().Contains(request.StudioName.ToLower()));
            }
            
            if (request.ServiceName != null) 
            {
                locations = locations.Where(l => l.LocationIndustries.Any(y => y.Industry.Name.ToLower().Contains(request.ServiceName.ToLower())));
            }

            if (request.IsHomePage != false) 
            {
                if (locations.Count() > MaxLocationsCount) 
                {
                    locations = locations.Take(MaxLocationsCount);
                }  
            }                    

            return new LocationsFilteredListViewModel
            {
                Locations = await locations.Where(c => c.IsDeleted != true).ProjectTo<LocationFilteredViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}