namespace Studio.Application.Employees.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

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

            var hasAppointments = this.context.Appointments.Where(a => a.IsDeleted != true).Any(a => a.EmployeeId == employee.Id && a.Employee.IsDeleted != true);

            if (hasAppointments)
            {
                throw new DeleteFailureException(GConst.Employee, request.Id, string.Format(GConst.DeleteException, GConst.Appointments, GConst.EmployeeLower));
            }   

            var hasServices = this.context.EmployeeServices.Where(es => es.IsDeleted != true).Any(a => a.EmployeeId == employee.Id && a.Employee.IsDeleted == false);

            if (hasServices)
            {
                throw new DeleteFailureException(GConst.Employee, request.Id, string.Format(GConst.DeleteException, GConst.Services, GConst.EmployeeLower));
            }          

            employee.DeletedOn = DateTime.UtcNow;
            employee.IsDeleted = true;
            
            this.context.Employees.Update(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
