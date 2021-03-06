namespace Studio.Application.Cities.Queries.GetEmployeesByLocation
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetEmployeesByLocationListQueryHandler : IRequestHandler<GetEmployeesByLocationListQuery, EmployeesByLocationListViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetEmployeesByLocationListQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<EmployeesByLocationListViewModel> Handle(GetEmployeesByLocationListQuery request, CancellationToken cancellationToken)
        {
            var employees = this.context.Employees.Where(e => e.LocationId == request.LocationId);

            if (employees == null) 
            {
                throw new NotFoundException(GConst.Employee, request.LocationId);
            }

            return new EmployeesByLocationListViewModel
            {
                Employees = await employees.Where(c => c.IsDeleted != true).ProjectTo<EmployeeByLocationViewModel>(this.mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}