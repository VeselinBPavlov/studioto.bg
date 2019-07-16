namespace Studio.Application.Appointments.Queries.GetAllAppointments
{
    using System.Collections.Generic;

    public class AppointmentsListViewModel
    {
        public IList<AppointmentAllViewModel> Appointments { get; set; } 
    }
}