namespace Studio.Application.EmployeeServices.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Interfaces.Persistence;
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Common;
    using Studio.Domain.Entities;

    public class CreateEmployeeServiceCommandHandler : IRequestHandler<CreateEmployeeServiceCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateEmployeeServiceCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateEmployeeServiceCommand request, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees.FindAsync(request.EmployeeId);

            if (employee == null || employee.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.EmployeeService, request.EmployeeId, string.Format(GConst.RefereceException, GConst.EmployeeLower, request.EmployeeId));
            }

            var service = await this.context.Services.FindAsync(request.ServiceId);

            if (service == null || service.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.EmployeeService, request.ServiceId, string.Format(GConst.RefereceException, GConst.ServiceLower, request.ServiceId));
            }

            var employeeService = new EmployeeService
            {
                EmployeeId = employee.Id,
                Employee = employee,
                ServiceId = service.Id,
                Service = service,
                Price = request.Price,
                DurationInMinutes = request.DurationInMinutes,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.EmployeeServices.Add(employeeService);

            await this.context.SaveChangesAsync(cancellationToken);            

            return Unit.Value;
        }
    }
}
