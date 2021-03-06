namespace Studio.Application.Cities.Queries.GetAllCities
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllCitiesListQueryHandler : IRequestHandler<GetAllCitiesListQuery, CitiesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllCitiesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CitiesListViewModel> Handle(GetAllCitiesListQuery request, CancellationToken cancellationToken)
        {
            return new CitiesListViewModel
            {
                Cities = await this.context.Cities.Where(c => c.IsDeleted != true).ProjectTo<CityAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}