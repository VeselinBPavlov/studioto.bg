namespace Studio.Application.Locations.Queries.GetAllLocations
{
    using System;
    using System.Linq.Expressions;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class LocationAllViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string WorkDays { get; set; }

        public string WorkHours { get; set; }  

        public string Phone { get; set; }

        public string Company { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Location, LocationAllViewModel>()
                .ForMember(x => x.Address, y => y.MapFrom(src => src.Address.AddressFormat.ToString()))
                .ForMember(x => x.WorkDays, y => y.MapFrom(src => src.StartDay.ToString() + " - " + src.EndDay.ToString()))
                .ForMember(x => x.WorkHours, y => y.MapFrom(src => src.StartHour.ToString() + " - " + src.EndHour.ToString()))
                .ForMember(x => x.Company, y => y.MapFrom(src => src.Client.CompanyName));
        }
    }
}