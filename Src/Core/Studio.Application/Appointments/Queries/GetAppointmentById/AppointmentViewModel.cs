namespace Studio.Application.Appointments.Queries.GetAppointmentById
{
    using System;
    using System.Linq.Expressions;
    using Studio.Domain.Entities;

    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ReservationDate { get; set; }

        public string ReservationTime { get; set; }

        public string TimeBlockHelper { get; set; }

        public string Comment { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeNames { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string UserId { get; set; }

        public string UserNames { get; set; }

        public static Expression<Func<Appointment, AppointmentViewModel>> Projection
        {
            get
            {
                return appointment => new AppointmentViewModel
                {
                    Id = appointment.Id,
                    FirstName = appointment.User.FirstName,
                    LastName = appointment.User.LastName,
                    Email = appointment.User.Email,
                    Phone = appointment.User.PhoneNumber,
                    ReservationDate = appointment.ReservationDate.ToShortDateString(),
                    ReservationTime = appointment.ReservationTime.ToShortTimeString(),
                    TimeBlockHelper = appointment.TimeBlockHelper,
                    Comment = appointment.Comment,
                    EmployeeId = appointment.EmployeeId,
                    EmployeeNames = appointment.Employee.FirstName + " " + appointment.Employee.LastName,
                    ServiceId = appointment.ServiceId,
                    ServiceName = appointment.Service.Name,
                    UserId = appointment.UserId,
                    UserNames = appointment.User.FirstName + " " + appointment.User.LastName
                };
            }
        }

        public static AppointmentViewModel Create(Appointment appointment)
        {
            return Projection.Compile().Invoke(appointment);
        }
    }
}