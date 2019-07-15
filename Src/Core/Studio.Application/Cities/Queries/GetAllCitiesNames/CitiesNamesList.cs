namespace Studio.Application.Cities.Queries.GetAllNames
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
    using System.Threading;
    using System.Threading.Tasks;

    public class CitiesNamesListViewModel
    {
        public IList<CityNameViewModel> Cities { get; set; }        
    }

    public class CityNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<City, CityNameViewModel>();
        }
    }

    public class GetCitiesNamesListQuery : IRequest<CitiesNamesListViewModel>
    {
    }

    public class GetCitiesNamesListQueryHandler : IRequestHandler<GetCitiesNamesListQuery, CitiesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetCitiesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CitiesNamesListViewModel> Handle(GetCitiesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new CitiesNamesListViewModel
            {
                Cities = await this.context.Cities.ProjectTo<CityNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
