namespace Studio.Application.EmployeeServices.Commands.Update
{
    using Studio.Domain.Entities;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Exceptions;
    using Interfaces.Persistence;
    using System;
    using System.Linq;
    using Studio.Common;

    public class UpdateEmployeeServiceCommandHandler : IRequestHandler<UpdateEmployeeServiceCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateEmployeeServiceCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateEmployeeServiceCommand request, CancellationToken cancellationToken)
        {
            var employeeService = await this.context.EmployeeServices
                .SingleOrDefaultAsync(c => c.EmployeeId == request.EmployeeId && c.ServiceId == request.ServiceId && c.IsDeleted != true, cancellationToken);

            if (employeeService == null)
            {
                throw new NotFoundException(GConst.EmployeeService, $"{request.EmployeeId} - {request.ServiceId}");
            }

            employeeService.Price = request.Price;
            employeeService.DurationInMinutes = request.DurationInMinutes;            
            employeeService.ModifiedOn = DateTime.UtcNow;

            this.context.EmployeeServices.Update(employeeService);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
