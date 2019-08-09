namespace Studio.Application.EmployeeServices.Queries.GetAllEmployeeServices
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllEmployeeServicesListQueryHandler : IRequestHandler<GetAllEmployeeServicesListQuery, EmployeeServicesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllEmployeeServicesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeeServicesListViewModel> Handle(GetAllEmployeeServicesListQuery request, CancellationToken cancellationToken)
        {
            return new EmployeeServicesListViewModel
            {
                EmployeeServices = await this.context.EmployeeServices.ProjectTo<EmployeeServiceAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}