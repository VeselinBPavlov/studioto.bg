namespace Studio.Application.Appointments.Commands.Create
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Domain.Entities;
    using Exceptions;
    using HelperMethods;
    using Interfaces.Persistence;
    using MediatR;
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
            var user = await this.context.StudioUsers.FindAsync(request.UserId);

            if (user == null || user.IsDeleted == true)
            {
                throw new CreateFailureException(GConst.Appointment, request.UserId, string.Format(GConst.RefereceException, GConst.UserLower, request.UserId));
            }

            var service = await this.context.Services.FindAsync(request.ServiceId);

            if (service == null || service.IsDeleted == true)
            {
                throw new CreateFailureException(GConst.Appointment, request.ServiceId, string.Format(GConst.RefereceException, GConst.ServiceLower, request.ServiceId));
            }

            var employee = await this.context.Employees.Include(e => e.Location).SingleOrDefaultAsync(e => e.Id == request.EmployeeId);

            if (employee == null || employee.IsDeleted == true)
            {
                throw new CreateFailureException(GConst.Appointment, request.EmployeeId, string.Format(GConst.RefereceException, GConst.EmployeeLower, request.EmployeeId));
            }

            if (request.TimeBlockHelper == GConst.AllHoursBusy)
            {
                throw new CreateFailureException(GConst.Appointment, request.UserId, string.Format(GConst.NotAvalableHours, request.ReservationDate.ToShortDateString()));
            }

            // Set Time
            request.ReservationTime = DateTime.Parse(request.TimeBlockHelper);

            // CheckWorkingHours
            DateTime start = request.ReservationDate.Add(request.ReservationTime.Value.TimeOfDay);
            DateTime end = request.ReservationDate.Add(request.ReservationTime.Value.TimeOfDay).AddMinutes(double.Parse(this.context.EmployeeServices.Find(employee.Id, service.Id).DurationInMinutes));
            if (!AppointmentHelper.IsInWorkingHours(this.context, employee, start, end))
            {
                throw new CreateFailureException(GConst.Appointment, request.UserId, string.Format(GConst.InvalidAppointmentHourException, employee.Location.StartHour, employee.Location.EndHour));
            }

            // Check Appointment Clash
            string check = AppointmentHelper.ValidateNoAppoinmentClash(this.context, request);
            if (check != string.Empty)
            {
                throw new CreateFailureException(GConst.Appointment, request.UserId, check);
            }

            var appointment = new Appointment
            {
                ReservationTime = request.ReservationTime.Value,
                ReservationDate = request.ReservationDate,
                TimeBlockHelper = request.TimeBlockHelper,
                Comment = request.Comment,
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            this.context.Appointments.Add(appointment);

            await this.context.SaveChangesAsync(cancellationToken);

            await this.mediator.Publish(new CreateAppointmentCommandNotification { AppointmentId = appointment.Id }, cancellationToken);

            return Unit.Value;
        }

        ////private bool IsInWorkingHours(DateTime start, DateTime end)
        ////{
        ////    // check Not Saturday or Sunday
        ////    if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
        ////    {
        ////        return false;
        ////    }
        ////
        ////    TimeRange workingHours = new TimeRange(TimeTrim.Hour(start, int.Parse(context.Administrations.Find(2).Value)), TimeTrim.Hour(start, int.Parse(context.Administrations.Find(3).Value)));
        ////    return workingHours.HasInside(new TimeRange(start, end));
        ////}
        ////
        ////private string ValidateNoAppoinmentClash(CreateAppointmentCommand appointment)
        ////{
        ////    var apps = context.Appointments.ToList();
        ////    var appointments = context.Appointments.Where(x => x.EmployeeId == appointment.EmployeeId).ToList();
        ////
        ////    foreach (var item in appointments)
        ////    {
        ////        if (item.ReservationTime.ToShortTimeString() == appointment.ReservationTime.ToShortTimeString() && item.ReservationDate.ToShortDateString() == appointment.ReservationDate.ToShortDateString())
        ////        {
        ////            string errorMessage = String.Format(
        ////                GConst.ReservedHourException,
        ////                item.Employee.FirstName,
        ////                item.ReservationDate.ToShortDateString(),
        ////                item.ReservationTime.ToShortTimeString());
        ////            return errorMessage;
        ////        }
        ////    }
        ////    return String.Empty;
        ////}
    }
}
