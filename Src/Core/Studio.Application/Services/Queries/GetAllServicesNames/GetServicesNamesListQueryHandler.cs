namespace Studio.Application.Services.Queries.GetAllServicesNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetServicesNamesListQueryHandler : IRequestHandler<GetServicesNamesListQuery, ServicesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetServicesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServicesNamesListViewModel> Handle(GetServicesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new ServicesNamesListViewModel
            {
                Services = await this.context.Services.Where(c => c.IsDeleted != true).OrderBy(x => x.Name).ProjectTo<ServiceNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
