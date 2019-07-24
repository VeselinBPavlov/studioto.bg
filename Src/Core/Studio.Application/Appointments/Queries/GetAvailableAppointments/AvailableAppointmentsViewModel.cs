namespace Studio.Application.Appointments.Queries.GetAvailableAppointments
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AvailableAppointmentsViewModel 
    {
        public List<SelectListItem> AvailableAppointments { get; set; }
    }
}