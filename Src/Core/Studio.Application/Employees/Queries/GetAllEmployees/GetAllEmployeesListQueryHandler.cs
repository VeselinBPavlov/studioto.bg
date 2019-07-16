namespace Studio.Application.Employees.Queries.GetAllEmployees
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Interfaces.Persistence;

    public class GetAllEmployeesListQueryHandler : IRequestHandler<GetAllEmployeesListQuery, EmployeesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetAllEmployeesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeesListViewModel> Handle(GetAllEmployeesListQuery request, CancellationToken cancellationToken)
        {
            return new EmployeesListViewModel
            {
                Employees = await this.context.Employees.Where(c => c.IsDeleted != true).ProjectTo<EmployeeAllViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}