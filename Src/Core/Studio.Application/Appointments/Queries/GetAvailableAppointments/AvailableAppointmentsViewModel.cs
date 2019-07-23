namespace Studio.Application.Appointments.Queries.GetAvailableAppointments
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class AvailableAppointmentsViewModel 
    {
        public  List<SelectListItem> AvailableAppointments { get; set; }
    }
}