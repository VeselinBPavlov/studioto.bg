namespace Studio.Application.EmployeeServices.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System.Linq;
    using Studio.Application.Exceptions;
    using System;
    using Studio.Common;

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

            await this.mediator.Publish(new CreateEmployeeServiceCommandNotification { EmployeeId = employeeService.EmployeeId, ServiceId = employeeService.ServiceId }, cancellationToken);

            return Unit.Value;
        }
    }
}
