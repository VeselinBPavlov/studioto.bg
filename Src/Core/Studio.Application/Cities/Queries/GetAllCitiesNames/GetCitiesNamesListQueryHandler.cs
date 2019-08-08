namespace Studio.Application.Cities.Queries.GetAllCitiesNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

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
                Cities = await this.context.Cities.Where(c => c.IsDeleted != true).OrderBy(x => x.Name).ProjectTo<CityNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
