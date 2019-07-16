namespace Studio.Application.Countries.Queries.GetAllCountries
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class CountryAllViewModel : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Country, CountryAllViewModel>();
        }
    }
}