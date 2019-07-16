namespace Studio.Application.Locations.Queries.GetAllNames
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class LocationsNamesListViewModel
    {
        public IList<LocationNameViewModel> Locations { get; set; }        
    }

    public class LocationNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Location, LocationNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.Client.CompanyName + " / " + src.Name));
        }
    }

    public class GetLocationsNamesListQuery : IRequest<LocationsNamesListViewModel>
    {
    }

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
