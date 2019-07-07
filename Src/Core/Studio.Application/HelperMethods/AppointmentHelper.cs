using System;
using System.Collections.Generic;
using System.Linq;
using Itenso.TimePeriod;
using Microsoft.AspNetCore.Mvc.Rendering;
using Studio.Application.Extensions;
using Studio.Application.Interfaces.Persistence;
using Studio.Domain.Entities;

namespace Studio.Application.HelperMethods
{
    public class AppointmentHelper
    {        
        //Appointment Creation Helper Methods
        //Checking if InsideWorkingHours + Not Weekend
        public static bool IsInWorkingHours(IStudioDbContext context, DateTime start, DateTime end)
        {
            // check Not Saturday or Sunday
            if (start.DayOfWeek == DayOfWeek.Saturday || start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            
            TimeRange workingHours = new TimeRange(TimeTrim.Hour(start, int.Parse(context.Administrations.Find(2).Value)), TimeTrim.Hour(start, int.Parse(context.Administrations.Find(3).Value)));
            return workingHours.HasInside(new TimeRange(start, end));
        }

        public static bool IsInWorkingHours(IStudioDbContext context, TimeBlock block)
        {
            // check Not Saturday or Sunday
            if (block.Start.DayOfWeek == DayOfWeek.Saturday || block.Start.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            TimeRange workingHours = new TimeRange(TimeTrim.Hour(block.Start.Date, int.Parse(context.Administrations.Find(2).Value)), TimeTrim.Hour(block.Start.Date, int.Parse(context.Administrations.Find(3).Value)));
            return workingHours.HasInside(block);
        }

        public static string ValidateNoAppoinmentClash(IStudioDbContext context, Appointment appointment)
        {
            var appointments = context.Appointments.Where(x => x.EmployeeId == appointment.EmployeeId);
                               
            foreach (var item in appointments)
            {
                if (item.ReservationTime.ToShortTimeString() == appointment.ReservationTime.ToShortTimeString() && item.ReservationDate.ToShortDateString() == appointment.ReservationDate.ToShortDateString())
                {
                    string errorMessage = String.Format(
                        "{0} already has an appointment on {1} on {2}.",
                        item.Employee.FirstName,
                        item.ReservationDate.ToShortDateString(),
                        item.ReservationTime.ToShortTimeString());
                    return errorMessage;
                }
            }
            return String.Empty;
        }

        public static List<SelectListItem> AvailableAppointments(IStudioDbContext context, int employeeId, DateTime date)
        {
            int A, S, E;
            A = int.Parse(context.Administrations.Find(1).Value);
            S = int.Parse(context.Administrations.Find(2).Value);
            E = int.Parse(context.Administrations.Find(3).Value);
            TimeBlock timeBlock = new TimeBlockExtension
                (
                new DateTime(date.Year, date.Month, date.Day, S, 0, 0),
                new TimeSpan(0, A, 0)
                );
            List<SelectListItem> ItemsList = new List<SelectListItem>();
            while (timeBlock.Start.CompareTo(DateTime.Now) <= 0) // No Appointments for past!!
            {
                timeBlock.Move(new TimeSpan(0, A, 0));
                if (!IsInWorkingHours(context, timeBlock))
                    break;
            }
            var appointments = context.Appointments.Where(x => x.EmployeeId == employeeId);
                               
            bool overlaps = false;
            while (IsInWorkingHours(context, timeBlock))
            {
                foreach (var appointment in appointments)
                {
                    TimeBlock BookedTimeBlock = new TimeBlockExtension
                (
                (appointment.ReservationDate.Date.Add(appointment.ReservationTime.TimeOfDay)),
                new TimeSpan(0, A, 0)
                );
                    if (BookedTimeBlock.OverlapsWith(timeBlock))
                    {
                        overlaps = true;
                    }
                }
                if (!overlaps)
                {
                    ItemsList.Add(new SelectListItem() { Text = timeBlock.ToString(), Value = timeBlock.Start.ToString("HH:mm") });

                }
                overlaps = false;
                timeBlock.Move(new TimeSpan(0, A, 0));
            }
            if (ItemsList.Count != 0)
            {
                return ItemsList;
            }
            ItemsList.Add(new SelectListItem() { Text = "No Appointments Available", Value = "DONT" });
            return ItemsList;
        }
    }
}