namespace Studio.Application.Appointments.Queries.GetAvailableAppointments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Itenso.TimePeriod;
    using MediatR;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Studio.Application.Extensions;
    using Studio.Application.HelperMethods;
    using Studio.Application.Interfaces.Persistence;
    using Studio.Common;

    public class GetAvailableAppontmentsQueryHandler : IRequestHandler<GetAvailableAppointmentsQuery, AvailableAppointmentsViewModel>
    {
        private readonly IStudioDbContext context;

        public GetAvailableAppontmentsQueryHandler(IStudioDbContext context)
        {
            this.context = context;
        }

        public async Task<AvailableAppointmentsViewModel> Handle(GetAvailableAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var command = request.Command;
            var availableAppointments = new AvailableAppointmentsViewModel();

            var employeeService = await this.context.EmployeeServices.FindAsync(command.EmployeeId, command.ServiceId);
            var employee = await this.context.Employees.Include(emp => emp.Location).SingleOrDefaultAsync(emp => emp.Id == command.EmployeeId);
            var startHour = employee.Location.StartHour;
            var endHour = employee.Location.EndHour;
            var duration = employeeService.DurationInMinutes;

            int a, s, e;
            a = int.Parse(duration);
            s = int.Parse(startHour);
            e = int.Parse(endHour);

            TimeBlock timeBlock = new TimeBlockExtension(new DateTime(command.ReservationDate.Year, command.ReservationDate.Month, command.ReservationDate.Day, s, 0, 0), new TimeSpan(0, a, 0));

            List<SelectListItem> itemsList = new List<SelectListItem>();

            // No Appointments for past!!
            while (timeBlock.Start.CompareTo(DateTime.Now) <= 0)
            {
                timeBlock.Move(new TimeSpan(0, a, 0));
                if (!AppointmentHelper.IsInWorkingHours(startHour, endHour, timeBlock))
                {
                    break;
                }
            }

            var appointments = this.context.Appointments.Where(x => x.EmployeeId == command.EmployeeId);
                               
            bool overlaps = false;
            while (AppointmentHelper.IsInWorkingHours(startHour, endHour, timeBlock))
            {
                foreach (var appointment in appointments)
                {
                    TimeBlock BookedTimeBlock = new TimeBlockExtension(appointment.ReservationDate.Date.Add(appointment.ReservationTime.TimeOfDay), new TimeSpan(0, a, 0));
                    if (BookedTimeBlock.OverlapsWith(timeBlock))
                    {
                        overlaps = true;
                    }
                }

                if (!overlaps)
                {
                    itemsList.Add(new SelectListItem() { Text = timeBlock.ToString(), Value = timeBlock.Start.ToString("HH:mm") });
                }

                overlaps = false;
                timeBlock.Move(new TimeSpan(0, a, 0));
            }

            if (itemsList.Count != 0)
            {
                availableAppointments.AvailableAppointments = itemsList;
                return availableAppointments;
            }

            itemsList.Add(new SelectListItem() { Text = "No Appointments Available", Value = GConst.AllHoursBusy });

            availableAppointments.AvailableAppointments = itemsList;
            return availableAppointments;
        }
    }
}