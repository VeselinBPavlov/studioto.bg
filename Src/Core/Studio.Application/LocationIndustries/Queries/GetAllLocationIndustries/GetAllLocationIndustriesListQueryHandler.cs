namespace Studio.Application.LocationIndustries.Queries.GetAllLocationIndustries
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllLocationIndustriesListQueryHandler : IRequestHandler<GetAllLocationIndustriesListQuery, LocationIndustriesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllLocationIndustriesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<LocationIndustriesListViewModel> Handle(GetAllLocationIndustriesListQuery request, CancellationToken cancellationToken)
        {
            return new LocationIndustriesListViewModel
            {
                LocationIndustries = await this.context.LocationIndustries.ProjectTo<LocationIndustryAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}