namespace Studio.Application.Appointments.Commands.Update
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
    using Studio.Application.HelperMethods;

    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Unit>
    {
        private readonly IStudioDbContext context;

        public UpdateAppointmentCommandHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await this.context.Appointments
                .SingleOrDefaultAsync(c => c.Id == request.Id && c.IsDeleted != true, cancellationToken);

            if (appointment == null)
            {
                throw new NotFoundException(GConst.Appointment, request.Id);
            }

            var service = await this.context.Services.FindAsync(request.ServiceId);

            if (service == null || service.IsDeleted == true)
            {
                throw new UpdateFailureException(GConst.Appointment, request.Id, string.Format(GConst.RefereceException, GConst.ServiceLower, request.ServiceId));
            }

            var employee = await this.context.Employees.FindAsync(request.EmployeeId);

            if (employee == null || employee.IsDeleted == true)
            {
                throw new UpdateFailureException(GConst.Appointment, request.Id, string.Format(GConst.RefereceException, GConst.EmployeeLower, request.EmployeeId));
            }

            //Set Time
            request.ReservationTime = DateTime.Parse(request.TimeBlockHelper);

            //CheckWorkingHours
            DateTime start = request.ReservationDate.Add(request.ReservationTime.TimeOfDay);
            DateTime end = (request.ReservationDate.Add(request.ReservationTime.TimeOfDay)).AddMinutes(double.Parse(context.EmployeeServices.Find(employee.Id, service.Id).DurationInMinutes));
            if (!(AppointmentHelper.IsInWorkingHours(context, employee, start, end)))
            {
                throw new UpdateFailureException(GConst.Appointment, request.Email, string.Format(GConst.InvalidAppointmentHourException, employee.Location.StartHour, employee.Location.EndHour));
            }

            appointment.FirstName = request.FirstName;
            appointment.LastName = request.LastName;
            appointment.Email = request.Email;
            appointment.Phone = request.Phone;
            appointment.ReservationTime = request.ReservationTime;
            appointment.ReservationDate = request.ReservationDate;
            appointment.TimeBlockHelper = request.TimeBlockHelper;
            appointment.Comment = request.Comment;
            appointment.ServiceId = request.ServiceId;
            appointment.EmployeeId = request.EmployeeId;
            appointment.ModifiedOn = DateTime.UtcNow;

            this.context.Appointments.Update(appointment);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
