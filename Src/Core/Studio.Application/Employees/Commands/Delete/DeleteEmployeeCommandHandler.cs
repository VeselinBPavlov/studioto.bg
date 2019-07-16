namespace Studio.Application.Employees.Commands.Delete
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

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteEmployeeCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await this.context.Employees.FindAsync(request.Id);

            if (employee == null || employee.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Employee, request.Id);
            }

            var hasAppointments = this.context.Appointments.Any(a => a.EmployeeId == employee.Id && a.Employee.IsDeleted == false);

            if (hasAppointments)
            {
                throw new DeleteFailureException(Common.GConst.Employee, request.Id, string.Format(GConst.DeleteException, GConst.Appointments, GConst.EmployeeLower));
            }   

            var hasServices = this.context.EmployeeServices.Any(a => a.EmployeeId == employee.Id && a.Employee.IsDeleted == false);

            if (hasServices)
            {
                throw new DeleteFailureException(Common.GConst.Employee, request.Id, string.Format(GConst.DeleteException, GConst.Services, GConst.EmployeeLower));
            }          

            employee.DeletedOn = DateTime.UtcNow;
            employee.IsDeleted = true;
            
            this.context.Employees.Update(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
