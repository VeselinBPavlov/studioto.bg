namespace Studio.Application.Appointments.Queries.GetAllAppointments
{
    using System;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class AppointmentAllViewModel : IHaveCustomMapping
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

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeNames { get; set; }

        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Appointment, AppointmentAllViewModel>()
                .ForMember(x => x.ReservationDate, y => y.MapFrom(src => src.ReservationDate.ToShortDateString()))
                .ForMember(x => x.ReservationTime, y => y.MapFrom(src => src.ReservationTime.ToShortTimeString()))
                .ForMember(x => x.ServiceName, y => y.MapFrom(src => src.Service.Name))
                .ForMember(x => x.EmployeeNames, y => y.MapFrom(src => src.Employee.FirstName + " " + src.Employee.FirstName))
                .ForMember(x => x.UserEmail, y => y.MapFrom(src => src.User.Email));            
        }
    }
}