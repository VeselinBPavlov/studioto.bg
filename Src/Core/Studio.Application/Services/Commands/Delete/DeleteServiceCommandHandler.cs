namespace Studio.Application.Services.Commands.Delete
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteServiceCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await this.context.Services.FindAsync(request.Id);

            if (service == null || service.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Service, request.Id);
            }

            var hasAppointments = this.context.Appointments.Where(a => a.IsDeleted != true).Any(a => a.ServiceId == service.Id && a.Service.IsDeleted == false);

            if (hasAppointments)
            {
                throw new DeleteFailureException(GConst.Service, request.Id, string.Format(GConst.DeleteException, GConst.Appointments, GConst.ServiceLower));
            }

            var hasEmployees = this.context.EmployeeServices.Where(es => es.IsDeleted != true).Any(s => s.ServiceId == service.Id && s.Employee.IsDeleted == false);

            if (hasEmployees)
            {
                throw new DeleteFailureException(GConst.Service, request.Id, string.Format(GConst.DeleteException, GConst.Employees, GConst.ServiceLower));
            }

            service.DeletedOn = DateTime.UtcNow;
            service.IsDeleted = true;
            
            this.context.Services.Update(service);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
