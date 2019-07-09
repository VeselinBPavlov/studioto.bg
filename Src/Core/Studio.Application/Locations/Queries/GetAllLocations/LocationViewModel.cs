namespace Studio.Application.Locations.Queries.GetAllLocations
{
    using System;
    using System.Linq.Expressions;
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class LocationViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string StartDay { get; set; }

        public string EndDay { get; set; }

        public string StartHour { get; set; }  

        public string EndHour { get; set; }   

        public string Phone { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Location, LocationViewModel>()
                .ForMember(x => x.Address, y => y.MapFrom(src => src.Address.AddressFormat.ToString()))
                .ForMember(x => x.StartDay, y => y.MapFrom(src => src.StartDay.ToString()))
                .ForMember(x => x.EndDay, y => y.MapFrom(src => src.EndDay.ToString()));
        }
    }
}