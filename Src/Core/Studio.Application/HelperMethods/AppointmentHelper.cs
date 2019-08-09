namespace Studio.Application.HelperMethods
{
    using System;
    using System.Linq;
    using Common;
    using Domain.Entities;
    using Interfaces.Core;
    using Interfaces.Persistence;
    using Itenso.TimePeriod;

    public class AppointmentHelper
    {        
        // Appointment Creation Helper Methods
        // Checking if InsideWorkingHours + Not Weekend
        public static bool IsInWorkingHours(IStudioDbContext context, Employee employee, DateTime start, DateTime end)
        {
            var startDay = ((int)start.DayOfWeek == 0) ? 7 : (int)start.DayOfWeek;
            var endDay = ((int)end.DayOfWeek == 0) ? 7 : (int)end.DayOfWeek;
            var locationEndDay = (int)employee.Location.EndDay;

            if (startDay > locationEndDay || endDay > locationEndDay)
            {
                return false;
            }
            
            TimeRange workingHours = new TimeRange(TimeTrim.Hour(start, int.Parse(employee.Location.StartHour)), TimeTrim.Hour(start, int.Parse(employee.Location.EndHour)));
            return workingHours.HasInside(new TimeRange(start, end));
        }

        public static bool IsInWorkingHours(string startHour, string endHour, TimeBlock block, Employee employee)
        {
            var start = ((int)block.Start.DayOfWeek == 0) ? 7 : (int)block.Start.DayOfWeek;
            var end = ((int)block.Start.DayOfWeek == 0) ? 7 : (int)block.Start.DayOfWeek;
            var locationEnd = (int)employee.Location.EndDay;

            if (start > locationEnd || end > locationEnd)
            {
                return false;
            }

            TimeRange workingHours = new TimeRange(TimeTrim.Hour(block.Start.Date, int.Parse(startHour)), TimeTrim.Hour(block.Start.Date, int.Parse(endHour)));
            return workingHours.HasInside(block);
        }

        public static string ValidateNoAppoinmentClash(IStudioDbContext context, IAppointmentCommand appointment)
        {
            var appointments = context.Appointments.Where(x => x.EmployeeId == appointment.EmployeeId && x.IsDeleted != true);
                               
            foreach (var item in appointments)
            {
                if (item.ReservationTime.ToShortTimeString() == appointment.ReservationTime.Value.ToShortTimeString() && item.ReservationDate.ToShortDateString() == appointment.ReservationDate.ToShortDateString())
                {
                    string errorMessage = string.Format(
                        GConst.ClashAppointmentMessage,
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