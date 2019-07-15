namespace Studio.Application.Employees.Queries.GetEmployeeById
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

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeViewModel>
    {
        private readonly IStudioDbContext context;

        public GetEmployeeByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeViewModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await context.Employees.Include(e => e.Location).SingleOrDefaultAsync(e => e.Id == request.Id);

            if (employee == null)
            {
                throw new NotFoundException(GConst.Employee, request.Id);
            }

            return EmployeeViewModel.Create(employee);
        }
    }
}