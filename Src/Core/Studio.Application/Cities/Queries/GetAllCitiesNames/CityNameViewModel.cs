namespace Studio.Application.Cities.Queries.GetAllCitiesNames
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class CityNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<City, CityNameViewModel>();
        }
    }
}
