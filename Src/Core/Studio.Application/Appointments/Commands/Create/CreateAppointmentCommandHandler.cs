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
    using Itenso.TimePeriod;
    using Studio.Application.HelperMethods;
    using Studio.Application.Interfaces.Core;

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

            if (request.TimeBlockHelper == GConst.AllHoursBusy)
            {
                throw new CreateFailureException(GConst.Appointment, request.Email, string.Format(GConst.NotAvalableHours, request.ReservationDate.ToShortDateString()));
            }

            //Set Time
            request.ReservationTime = DateTime.Parse(request.TimeBlockHelper);

            //CheckWorkingHours
            DateTime start = request.ReservationDate.Add(request.ReservationTime.TimeOfDay);
            DateTime end = (request.ReservationDate.Add(request.ReservationTime.TimeOfDay)).AddMinutes(double.Parse(context.EmployeeServices.Find(employee.Id, service.Id).DurationInMinutes));
            if (!(AppointmentHelper.IsInWorkingHours(context, employee, start, end)))
            {
                throw new CreateFailureException(GConst.Appointment, request.Email, string.Format(GConst.InvalidAppointmentHourException, employee.Location.StartHour, employee.Location.EndHour));
            }

            //Check Appointment Clash
            string check = AppointmentHelper.ValidateNoAppoinmentClash(context, request);
            if (check != "")
            {
                throw new CreateFailureException(GConst.Appointment, request.Email, check);
            }

            var appointment = new Appointment
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                ReservationTime = request.ReservationTime,
                ReservationDate = request.ReservationDate,
                TimeBlockHelper = request.TimeBlockHelper,
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

        //private bool IsInWorkingHours(DateTime start, DateTime end)
        //{
        //    // check Not Saturday or Sunday
        //    if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        return false;
        //    }

        //    TimeRange workingHours = new TimeRange(TimeTrim.Hour(start, int.Parse(context.Administrations.Find(2).Value)), TimeTrim.Hour(start, int.Parse(context.Administrations.Find(3).Value)));
        //    return workingHours.HasInside(new TimeRange(start, end));
        //}

        //private string ValidateNoAppoinmentClash(CreateAppointmentCommand appointment)
        //{
        //    var apps = context.Appointments.ToList();
        //    var appointments = context.Appointments.Where(x => x.EmployeeId == appointment.EmployeeId).ToList();

        //    foreach (var item in appointments)
        //    {
        //        if (item.ReservationTime.ToShortTimeString() == appointment.ReservationTime.ToShortTimeString() && item.ReservationDate.ToShortDateString() == appointment.ReservationDate.ToShortDateString())
        //        {
        //            string errorMessage = String.Format(
        //                GConst.ReservedHourException,
        //                item.Employee.FirstName,
        //                item.ReservationDate.ToShortDateString(),
        //                item.ReservationTime.ToShortTimeString());
        //            return errorMessage;
        //        }
        //    }
        //    return String.Empty;
        //}
    }
}
