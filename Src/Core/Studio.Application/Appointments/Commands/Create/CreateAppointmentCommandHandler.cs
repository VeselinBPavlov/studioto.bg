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
    using Microsoft.EntityFrameworkCore;

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
            StudioUser user = new StudioUser();

            if (request.UserId == null)
            {
                user = await this.context.StudioUsers.SingleOrDefaultAsync(u => u.Email == request.Email);

                if (user == null)
                {
                    user = new StudioUser
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Email = request.Email,
                        NormalizedEmail = request.Email.ToUpper(),
                        PhoneNumber = request.Phone,
                        CreatedOn = DateTime.UtcNow,
                        IsDeleted = false,
                        IsTemporary = true
                    };

                    context.StudioUsers.Add(user);
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                user = await this.context.StudioUsers.FindAsync(request.UserId);
            }

            var service = await this.context.Services.FindAsync(request.ServiceId);

            if (service == null || service.IsDeleted == true)
            {
                throw new CreateFailureException(GConst.Appointment, request.ServiceId, string.Format(GConst.RefereceException, GConst.ServiceLower, request.ServiceId));
            }

            var employee = await this.context.Employees.FindAsync(request.EmployeeId);

            if (employee == null || employee.IsDeleted == true)
            {
                throw new CreateFailureException(GConst.Appointment, request.EmployeeId, string.Format(GConst.RefereceException, GConst.EmployeeLower, request.EmployeeId));
            }

            var appointment = new Appointment
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                ReservationTime = request.ReservetionTime,
                Comment = request.Comment,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Appointments.Add(appointment);

            await context.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new CreateAppointmentCommandNotification { AppointmentId = appointment.Id }, cancellationToken);

            return Unit.Value;
        }
    }
}
