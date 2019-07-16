namespace Studio.Application.Appointments.Commands.Delete
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

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        private readonly IStudioDbContext context;

        public DeleteAppointmentCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await this.context.Appointments.FindAsync(request.Id);

            if (appointment == null || appointment.IsDeleted == true)
            {
                throw new NotFoundException(GConst.Appointment, request.Id);
            }         

            appointment.DeletedOn = DateTime.UtcNow;
            appointment.IsDeleted = true;
            
            this.context.Appointments.Update(appointment);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
