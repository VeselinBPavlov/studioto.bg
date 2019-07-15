namespace Studio.Application.LocationIndustries.Queries.GetAllLocationIndustries
{
    using AutoMapper;
    using Studio.Application.Interfaces.Mapping;
    using Studio.Domain.Entities;

    public class LocationIndustryAllViewModel : IHaveCustomMapping
    {
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        public int IndustryId { get; set; }

        public string IndustryName { get; set; }

        public string Description { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<LocationIndustry, LocationIndustryAllViewModel>();
        }
    }
}