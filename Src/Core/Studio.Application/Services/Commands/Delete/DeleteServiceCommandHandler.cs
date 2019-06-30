namespace Studio.Application.Services.Commands.Delete
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

    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteServiceCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await context.Services.FindAsync(request.Id);

            if (service == null)
            {
                throw new NotFoundException(GConst.Service, request.Id);
            }

            var hasAppointments = this.context.Appointments.Any(s => s.ServiceId == service.Id);

            if (hasAppointments)
            {
                throw new DeleteFailureException(GConst.Service, request.Id, string.Format(GConst.DeleteException, GConst.Appointments, GConst.ServiceLower));
            }

            var hasEmployees = this.context.EmployeeServices.Any(s => s.ServiceId == service.Id);

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
