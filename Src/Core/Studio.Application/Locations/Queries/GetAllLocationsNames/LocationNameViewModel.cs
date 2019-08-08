namespace Studio.Application.Locations.Queries.GetAllLocationsNames
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class LocationNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Location, LocationNameViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.Client.CompanyName + " / " + src.Name));
        }
    }
}
