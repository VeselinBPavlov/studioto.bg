namespace Studio.Application.Cities.Queries.GetEmployeeServiceById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Countries.Queries.GetCountryById;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetEmployeeServiceByIdQueryHandler : IRequestHandler<GetEmployeeServiceByIdQuery, EmployeeServiceViewModel>
    {
        private readonly IStudioDbContext context;

        public GetEmployeeServiceByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeServiceViewModel> Handle(GetEmployeeServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var employeeService = await context.EmployeeServices
                .Include(c => c.Employee)
                .Include(c => c.Service)
                .SingleOrDefaultAsync(c => c.EmployeeId == request.EmployeeId && c.ServiceId == request.ServiceId);

            if (employeeService == null)
            {
                throw new NotFoundException(GConst.EmployeeService, request.EmployeeId + "/" + request.ServiceId);
            }

            return EmployeeServiceViewModel.Create(employeeService);
        }
    }
}