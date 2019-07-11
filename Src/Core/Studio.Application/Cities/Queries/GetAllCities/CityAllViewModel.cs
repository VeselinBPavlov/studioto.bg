namespace Studio.Application.Cities.Queries.GetAllCities
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class CityAllViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<City, CityAllViewModel>()
                .ForMember(x => x.CountryName, y => y.MapFrom(src => src.Country.Name));
        }
    }
}