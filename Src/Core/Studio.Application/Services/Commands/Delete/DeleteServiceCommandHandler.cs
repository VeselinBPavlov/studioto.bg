namespace Studio.Application.Services.Commands.Delete
{
    using MediatR;
    using Studio.Application.Exceptions;
    using Studio.Application.Interfaces.Persistence;
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
                throw new NotFoundException(nameof(Service), request.Id);
            }

            var hasAppointments = this.context.Appointments.Any(s => s.ServiceId == service.Id);

            if (hasAppointments)
            {
                throw new DeleteFailureException(nameof(Service), request.Id, "There are existing appointments associated with this service.");
            }

            var hasEmployees = this.context.EmployeeServices.Any(s => s.ServiceId == service.Id);

            if (hasEmployees)
            {
                throw new DeleteFailureException(nameof(Service), request.Id, "There are existing employees associated with this service.");
            }

            service.DeletedOn = DateTime.UtcNow;
            service.IsDeleted = true;
            
            this.context.Services.Update(service);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
