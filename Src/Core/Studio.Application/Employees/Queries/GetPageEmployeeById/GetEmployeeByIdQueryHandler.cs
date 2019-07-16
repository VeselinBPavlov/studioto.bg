namespace Studio.Application.Employees.Queries.GetPageEmployeeById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Employees.Queries.GetPageEmployeeById;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeProfileViewModel>
    {
        private readonly IStudioDbContext context;

        public GetEmployeeByIdQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeProfileViewModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees.FindAsync(request.Id);

            if (employee == null)
            {
                throw new NotFoundException(GConst.Employee, request.Id);
            }

            return EmployeeProfileViewModel.Create(employee);
        }
    }
}