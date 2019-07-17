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

        // public static bool IsInWorkingHours(IStudioDbContext context, TimeBlock block)
        // {
        //     // check Not Saturday or Sunday
        //     if (block.Start.DayOfWeek == DayOfWeek.Saturday || block.Start.DayOfWeek == DayOfWeek.Sunday)
        //     {
        //         return false;
        //     }

        //     TimeRange workingHours = new TimeRange(TimeTrim.Hour(block.Start.Date, int.Parse(context.Administrations.Find(2).Value)), TimeTrim.Hour(block.Start.Date, int.Parse(context.Administrations.Find(3).Value)));
        //     return workingHours.HasInside(block);
        // }

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

        // public static List<SelectListItem> AvailableAppointments(IStudioDbContext context, int employeeId, DateTime date)
        // {
        //     int a, s, e;
        //     a = int.Parse(context.Administrations.Find(1).Value);
        //     s = int.Parse(context.Administrations.Find(2).Value);
        //     e = int.Parse(context.Administrations.Find(3).Value);

        //     TimeBlock timeBlock = new TimeBlockExtension(new DateTime(date.Year, date.Month, date.Day, s, 0, 0), new TimeSpan(0, a, 0));
        //     List<SelectListItem> ItemsList = new List<SelectListItem>();

        //     // No Appointments for past!!
        //     while (timeBlock.Start.CompareTo(DateTime.Now) <= 0)
        //     {
        //         timeBlock.Move(new TimeSpan(0, a, 0));
        //         if (!IsInWorkingHours(context, timeBlock))
        //         {
        //             break;
        //         }
        //     }
        //     var appointments = context.Appointments.Where(x => x.EmployeeId == employeeId);
                               
        //     bool overlaps = false;
        //     while (IsInWorkingHours(context, timeBlock))
        //     {
        //         foreach (var appointment in appointments)
        //         {
        //             TimeBlock BookedTimeBlock = new TimeBlockExtension(appointment.ReservationDate.Date.Add(appointment.ReservationTime.TimeOfDay), new TimeSpan(0, a, 0));
        //             if (BookedTimeBlock.OverlapsWith(timeBlock))
        //             {
        //                 overlaps = true;
        //             }
        //         }

        //         if (!overlaps)
        //         {
        //             ItemsList.Add(new SelectListItem() { Text = timeBlock.ToString(), Value = timeBlock.Start.ToString("HH:mm") });
        //         }

        //         overlaps = false;
        //         timeBlock.Move(new TimeSpan(0, a, 0));
        //     }

        //     if (ItemsList.Count != 0)
        //     {
        //         return ItemsList;
        //     }

        //     ItemsList.Add(new SelectListItem() { Text = "No Appointments Available", Value = "DONT" });
        //     return ItemsList;
        // }
    }
}