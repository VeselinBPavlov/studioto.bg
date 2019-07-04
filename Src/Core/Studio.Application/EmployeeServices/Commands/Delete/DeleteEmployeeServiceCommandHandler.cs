namespace Studio.Application.EmployeeServices.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;
    using Studio.Domain.Entities;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteEmployeeServiceCommandHandler : IRequestHandler<DeleteEmployeeServiceCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteEmployeeServiceCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeServiceCommand request, CancellationToken cancellationToken)
        {
            var employeeService = await context.EmployeeServices.FindAsync(request.EmployeeId, request.ServiceId);

            if (employeeService == null || employeeService.IsDeleted == true)
            {
                throw new NotFoundException(GConst.EmployeeService, $"{request.EmployeeId} - {request.ServiceId}");
            }            

            employeeService.DeletedOn = DateTime.UtcNow;
            employeeService.IsDeleted = true;
            
            this.context.EmployeeServices.Update(employeeService);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
