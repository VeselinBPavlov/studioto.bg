namespace Studio.Application.Appointments.Commands.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Exceptions;
    using Interfaces.Persistence;
    using MediatR;

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
