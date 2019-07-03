namespace Studio.Application.Appointments.Commands.Create
{
    using MediatR;
    using Interfaces.Persistence;
    using System.Threading;
    using System.Threading.Tasks;
    using Studio.Domain.Entities;
    using System.Linq;
    using Studio.Application.Exceptions;
    using System;
    using Studio.Common;
    using System.Globalization;

    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Unit>
    {
        private readonly IStudioDbContext context;
        private readonly IMediator mediator;

        public CreateAppointmentCommandHandler(IStudioDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var service = await this.context.Services.FindAsync(request.ServiceId);

            if (service == null || service.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Appointment, request.ServiceId, string.Format(GConst.RefereceException, GConst.ServiceLower, request.ServiceId));
            }

            var employee = await this.context.Employees.FindAsync(request.UserId);

            if (employee == null || employee.IsDeleted == true) 
            {
                throw new CreateFailureException(GConst.Appointment, request.EmployeeId, string.Format(GConst.RefereceException, GConst.EmployeeLower, request.EmployeeId));
            }

            // TODO: User logic.

            var Appointment = new Appointment
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                ReservationTime = DateTime.ParseExact(request.ReservetionTime, "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture),
                Comment = request.Comment,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                UserId = request.UserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Appointments.Add(Appointment);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateAppointmentCommandNotification { AppointmentId = Appointment.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
