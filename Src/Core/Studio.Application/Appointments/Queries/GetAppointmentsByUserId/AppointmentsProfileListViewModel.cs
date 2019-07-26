namespace Studio.Application.Appointments.Queries.GetAppointmentsByUserId
{
    using System.Collections.Generic;

    public class AppointmentsProfileListViewModel
    {
        public IList<AppointmentProfileViewModel> NewAppointments { get; set; } 
        public IList<AppointmentProfileViewModel> OldAppointments { get; set; } 
    }
}