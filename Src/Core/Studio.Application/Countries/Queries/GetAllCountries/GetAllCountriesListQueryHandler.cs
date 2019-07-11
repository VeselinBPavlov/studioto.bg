namespace Studio.Application.Countries.Queries.GetAllCountries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllCountriesListQueryHandler : IRequestHandler<GetAllCountriesListQuery, CountriesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllCountriesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<CountriesListViewModel> Handle(GetAllCountriesListQuery request, CancellationToken cancellationToken)
        {
            return new CountriesListViewModel
            {
                Countries = await this.context.Countries.ProjectTo<CountryAllViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}