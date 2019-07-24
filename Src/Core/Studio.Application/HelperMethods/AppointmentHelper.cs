namespace Studio.Application.HelperMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Extensions;
    using Interfaces.Core;
    using Interfaces.Persistence;
    using Itenso.TimePeriod;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Studio.Domain.Entities;

    public class AppointmentHelper
    {        
        // Appointment Creation Helper Methods
        // Checking if InsideWorkingHours + Not Weekend
        public static bool IsInWorkingHours(IStudioDbContext context, Employee employee, DateTime start, DateTime end)
        {
            // check Not Saturday or Sunday
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            
            TimeRange workingHours = new TimeRange(TimeTrim.Hour(start, int.Parse(employee.Location.StartHour)), TimeTrim.Hour(start, int.Parse(employee.Location.EndHour)));
            return workingHours.HasInside(new TimeRange(start, end));
        }

        public static bool IsInWorkingHours(string startHour, string endHour, TimeBlock block)
        {
            // check Not Saturday or Sunday
            if (block.Start.DayOfWeek == DayOfWeek.Saturday || block.Start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            TimeRange workingHours = new TimeRange(TimeTrim.Hour(block.Start.Date, int.Parse(startHour)), TimeTrim.Hour(block.Start.Date, int.Parse(endHour)));
            return workingHours.HasInside(block);
        }

        public static string ValidateNoAppoinmentClash(IStudioDbContext context, IAppointmentCommand appointment)
        {
            var appointments = context.Appointments.Where(x => x.EmployeeId == appointment.EmployeeId);
                               
            foreach (var item in appointments)
            {
                if (item.ReservationTime.ToShortTimeString() == appointment.ReservationTime.Value.ToShortTimeString() && item.ReservationDate.ToShortDateString() == appointment.ReservationDate.ToShortDateString())
                {
                    string errorMessage = string.Format(
                        "{0} already has an appointment on {1} on {2}.",
                        item.Employee.FirstName,
                        item.ReservationDate.ToShortDateString(),
                        item.ReservationTime.ToShortTimeString());
                    return errorMessage;
                }
            }

            return string.Empty;
        }        
    }
}