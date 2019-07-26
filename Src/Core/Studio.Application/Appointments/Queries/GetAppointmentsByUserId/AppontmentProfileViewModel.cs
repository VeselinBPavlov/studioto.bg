namespace Studio.Application.Appointments.Queries.GetAppointmentsByUserId
{
    using System;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class AppointmentProfileViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public string ReservationDate { get; set; }

        public string ReservationTime { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeNames { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Appointment, AppointmentProfileViewModel>()
                .ForMember(x => x.ReservationDate, y => y.MapFrom(src => src.ReservationDate.ToShortDateString()))
                .ForMember(x => x.LocationName, y => y.MapFrom(src => src.Employee.Location.Name))
                .ForMember(x => x.ReservationTime, y => y.MapFrom(src => src.ReservationTime.ToShortTimeString()))
                .ForMember(x => x.ServiceName, y => y.MapFrom(src => src.Service.Name))
                .ForMember(x => x.EmployeeNames, y => y.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
        }
    }
}