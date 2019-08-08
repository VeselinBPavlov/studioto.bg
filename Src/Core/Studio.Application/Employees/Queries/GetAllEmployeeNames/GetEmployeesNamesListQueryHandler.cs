namespace Studio.Application.Employees.Queries.GetAllEmployeeNames
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Interfaces.Persistence;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetEmployeesNamesListQueryHandler : IRequestHandler<GetEmployeesNamesListQuery, EmployeesNamesListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetEmployeesNamesListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeesNamesListViewModel> Handle(GetEmployeesNamesListQuery request, CancellationToken cancellationToken)
        {
            return new EmployeesNamesListViewModel
            {
                Employees = await this.context.Employees.Where(c => c.IsDeleted != true).OrderBy(x => x.FirstName).ProjectTo<EmployeeNameViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
