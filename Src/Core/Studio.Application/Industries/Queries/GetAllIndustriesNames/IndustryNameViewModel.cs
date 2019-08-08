namespace Studio.Application.Industries.Queries.GetAllIndustriesNames
{
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Mapping;

    public class IndustryNameViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Industry, IndustryNameViewModel>();
        }
    }
}
