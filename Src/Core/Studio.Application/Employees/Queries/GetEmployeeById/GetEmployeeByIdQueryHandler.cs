namespace Studio.Application.Cities.Queries.GetEmployeeById
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

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeProfileViewModel>
    {
        private readonly IStudioDbContext context;
        private readonly IMapper mapper;

        public GetEmployeeByIdQueryHandler(IStudioDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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