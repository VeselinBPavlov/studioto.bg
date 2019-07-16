namespace Studio.Application.Services.Queries.GetAllServices
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllServicesListQueryHandler : IRequestHandler<GetAllServicesListQuery, ServicesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllServicesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServicesListViewModel> Handle(GetAllServicesListQuery request, CancellationToken cancellationToken)
        {
            return new ServicesListViewModel
            {
                Services = await this.context.Services.ProjectTo<ServiceAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}