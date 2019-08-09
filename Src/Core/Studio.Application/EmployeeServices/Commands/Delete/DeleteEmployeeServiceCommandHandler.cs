namespace Studio.Application.EmployeeServices.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class DeleteEmployeeServiceCommandHandler : IRequestHandler<DeleteEmployeeServiceCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteEmployeeServiceCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeServiceCommand request, CancellationToken cancellationToken)
        {
            var employeeService = await this.context.EmployeeServices.FindAsync(request.EmployeeId, request.ServiceId);

            if (employeeService == null)
            {
                throw new NotFoundException(GConst.EmployeeService, $"{request.EmployeeId} - {request.ServiceId}");
            }            
            
            this.context.EmployeeServices.Remove(employeeService);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
