namespace Studio.Application.EmployeeServices.Queries.GetServicesByEmployeeId
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetServicesByEmployeeIdListQueryHandler : IRequestHandler<GetServicesByEmployeeIdListQuery, ServicesByEmployeeIdListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetServicesByEmployeeIdListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServicesByEmployeeIdListViewModel> Handle(GetServicesByEmployeeIdListQuery request, CancellationToken cancellationToken)
        {
            return new ServicesByEmployeeIdListViewModel
            {
                Services = await this.context.EmployeeServices.Where(c => c.EmployeeId == request.EmployeeId).ProjectTo<ServiceByEmployeeIdViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}